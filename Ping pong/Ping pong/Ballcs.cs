using SFML.Graphics;
using SFML.System;

class Ball
{
    public CircleShape Shape { get; private set; }
    public Vector2f Velocity { get; set; }

    public Ball(Vector2f position, Vector2f initialVelocity)
    {
        Shape = new CircleShape(10)
        {
            FillColor = Color.White,
            Position = position
        };
        Velocity = initialVelocity;
    }

    public void Update(float windowWidth, float windowHeight)
    {
        Shape.Position += Velocity;

        if (Shape.Position.Y <= 0 || Shape.Position.Y + Shape.Radius * 2 >= windowHeight)
        {
            Velocity = new Vector2f(Velocity.X, -Velocity.Y);
        }
    }

    public void Reset(Vector2f position, Vector2f initialVelocity)
    {
        Shape.Position = position;
        Velocity = initialVelocity;
    }
}