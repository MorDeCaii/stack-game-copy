using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static bool isBlockInput;
    public static int score;
    public float maxSpeed;
    public float speedStep;
    public float cutTreshold;
    public GameObject piecePrefab;
    public BrickSpawner spawner;
    public Brick brickPrefab;
    public CameraController cameraController;
    public SkyController skyController;

    public UnityEvent onInit;
    public UnityEvent onStartScreen;
    public UnityEvent onGameStart;
    public UnityEvent onGameOver;

    private GameObject root;
    private Brick prevBrick;
    private GameState state;
    private Brick firstBrick;
    private Brick newBrick;
    private float curSpeed;

    void Start()
    {
        root = new GameObject("Root");
        prevBrick = brickPrefab;
        BlockInput();
        InitGame();
        curSpeed = 8f;
    }

    void InitGame()
    {
        score = 0;
        firstBrick = spawner.SpawnBrick(prevBrick, Vector3.zero, 0f, root.transform);
        firstBrick.transform.position = new Vector3(0, firstBrick.transform.position.y, 0);
        cameraController.FocusOnBrick(firstBrick);
        skyController.SetSkyByColorInstant(firstBrick.color);
        prevBrick = firstBrick;

        state = (int)GameState.StartScreen;
        onInit.Invoke();
    }

    public void StartGame()
    {
        score = 0;
        newBrick = spawner.SpawnBrick(prevBrick, prevBrick.transform.position, curSpeed);
        skyController.SetSkyByColor(newBrick.color);
    }

    void Update()
    {
        switch (state)
        {
            case GameState.StartScreen:
                if (!isBlockInput && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartGame();
                    SetState((int)GameState.Playing);
                }
                break;
            case GameState.Playing:
                if (!isBlockInput && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    HandleInput();
                }
                break;
            case GameState.GameOver:
                if (!isBlockInput && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    SetState((int)GameState.StartScreen);
                }
                break;
        }
    }

    public void SetState (int _state)
    {
        switch ((GameState)_state)
        {
            case GameState.StartScreen:
                onStartScreen.Invoke();
                break;
            case GameState.Playing:
                onGameStart.Invoke();
                break;
            case GameState.GameOver:
                onGameOver.Invoke();
                break;
        }
        state = (GameState)_state;
    }

    void HandleInput()
    {
        CutResult result = BrickCutter.CutBrick(newBrick, prevBrick, spawner.curAxis, cutTreshold);

        if (result.isSuccess)
        {
            score++;
            if (result.cutPiece == null)
            {
                Vector3 newPos = newBrick.transform.position;
                Vector3 prevPos = prevBrick.transform.position;
                newBrick.transform.position = new Vector3(prevPos.x, newPos.y, prevPos.z);
            }
            else
            {
                GameObject piece = Instantiate(piecePrefab, result.cutPiece.position, Quaternion.identity, root.transform);
                piece.transform.localScale = result.cutPiece.localScale;

                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                propertyBlock.SetColor("_BaseColor", newBrick.color);
                piece.GetComponent<Renderer>().SetPropertyBlock(propertyBlock);

                SetRandomMovement(piece, result.axis);
            }
        }
        else
        {
            BlockInput();
            GameObject piece = Instantiate(piecePrefab, newBrick.transform.position, Quaternion.identity, root.transform);
            piece.transform.localScale = newBrick.transform.localScale;

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_BaseColor", newBrick.color);
            piece.GetComponent<Renderer>().SetPropertyBlock(propertyBlock);

            Destroy(newBrick.gameObject);
            SetRandomMovement(piece, result.axis);

            SetState((int)GameState.GameOver);
            return;
        }

        prevBrick.MakeStatic();
        newBrick.movement.ToggletMovement();
        prevBrick = newBrick;
        prevBrick.transform.parent = root.transform;
        cameraController.FocusOnBrick(prevBrick);
        newBrick = spawner.SpawnBrick(prevBrick, prevBrick.transform.position, curSpeed);
        skyController.SetSkyByColor(newBrick.color);

        if (curSpeed < maxSpeed)
        {
            curSpeed += speedStep;
            if (curSpeed > maxSpeed) curSpeed = maxSpeed;
        }
    }

    void SetRandomMovement(GameObject go, BrickAxis axis)
    {
        float xVel = Random.Range(-4, 4);
        float yVel = Random.Range(-1, 1);
        float zVel = Random.Range(-1, 1);
        if (axis == BrickAxis.Z)
        {
            go.GetComponent<Rigidbody>().angularVelocity = new Vector3(xVel, yVel, zVel);
        }
        else
        {
            go.GetComponent<Rigidbody>().angularVelocity = new Vector3(zVel, yVel, xVel);
        }
    }

    public void FitStackToView()
    {
        float height = Vector3.Distance(firstBrick.transform.position, prevBrick.transform.position);
        float newScale = 12 / height;

        if (newScale < 1) root.transform.DOScale(12 / height, 0.6f);
        if (cameraController.transform.position.y > 16.8f) cameraController.transform.DOMoveY(16.8f, 0.6f);
    }

    public void ResetStack()
    {
        root.transform.DOMoveY(-30, 0.4f).OnComplete(() => 
        {
            Destroy(root);
            root = new GameObject("Root");
            prevBrick = brickPrefab;
            score = 0;
            curSpeed = 8f;
            firstBrick = spawner.SpawnBrick(prevBrick, Vector3.zero, 0f, root.transform);
            firstBrick.transform.position = new Vector3(0, firstBrick.transform.position.y, 0);
            cameraController.FocusOnBrick(firstBrick);
            skyController.SetSkyByColorInstant(firstBrick.color);
            prevBrick = firstBrick;
            root.transform.DOMoveY(0, 0.6f);
            Debug.Log("Reset stack");
        });
    }

    public static void BlockInput()
    {
        isBlockInput = true;
    }
    public static void UnblockInput()
    {
        isBlockInput = false;
    }

}