public interface IMovement
{
    void Move();
    void SetSpeed(float _speed);
    void SetAxis(BrickAxis _axis);
    void ToggletMovement();
}
