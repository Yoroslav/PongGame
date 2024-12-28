using SFML.Graphics;
using SFML.System;
using SFML.Window;

class PongGame
{
    private const int WindowWidth = 800;
    private const int WindowHeight = 600;

    private RenderWindow window;
    private RectangleShape player1Paddle;
    private RectangleShape player2Paddle;
    private Ball ball;
    private Scoreboard scoreboard;

    private Font font;
    private int player1Score = 0;
    private int player2Score = 0;

    public PongGame()
    {
        window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Pong Game");
        window.Closed += (sender, e) => window.Close();
        window.KeyPressed += OnKeyPressed;

        InitializeGame();
    }

    private void InitializeGame()
    {
        player1Paddle = new RectangleShape(new Vector2f(10, 100))
        {
            FillColor = Color.White,
            Position = new Vector2f(20, (WindowHeight - 100) / 2)
        };

        player2Paddle = new RectangleShape(new Vector2f(10, 100))
        {
            FillColor = Color.White,
            Position = new Vector2f(WindowWidth - 30, (WindowHeight - 100) / 2)
        };

        ball = new Ball(new Vector2f(WindowWidth / 2, WindowHeight / 2), new Vector2f(-4, -4));

        font = new Font(@"C:\Users\Yaroslav\OneDrive\PongGame\Ping pong\Ping pong\OpenSans.ttf");
        scoreboard = new Scoreboard(font, new Vector2f(WindowWidth / 2 - 30, 10));
    }

    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();
            Update();
            Draw();
        }
    }

    private void Update()
    {
        ball.Update(WindowWidth, WindowHeight);
        if (ball.Shape.GetGlobalBounds().Intersects(player1Paddle.GetGlobalBounds()) ||
            ball.Shape.GetGlobalBounds().Intersects(player2Paddle.GetGlobalBounds()))
        {
            ball.Velocity = new Vector2f(-ball.Velocity.X, ball.Velocity.Y);
        }

        if (ball.Shape.Position.X <= 0 || ball.Shape.Position.X + ball.Shape.Radius * 2 >= WindowWidth)
        {
            if (ball.Shape.Position.X <= 0)
            {
                player2Score++;
                ball.Reset(new Vector2f(WindowWidth / 2, WindowHeight / 2), new Vector2f(4, -4));
            }
            else
            {
                player1Score++;
                ball.Reset(new Vector2f(WindowWidth / 2, WindowHeight / 2), new Vector2f(-4, -4));
            }
        }

        scoreboard.UpdateScore(player1Score, player2Score);
        UpdatePaddlePosition(player1Paddle, Keyboard.Key.W, Keyboard.Key.S);
        UpdatePaddlePosition(player2Paddle, Keyboard.Key.Up, Keyboard.Key.Down);
    }

    private void UpdatePaddlePosition(RectangleShape paddle, Keyboard.Key upKey, Keyboard.Key downKey)
    {
        if (Keyboard.IsKeyPressed(upKey) && paddle.Position.Y > 0)
        {
            paddle.Position += new Vector2f(0, -5);
        }
        if (Keyboard.IsKeyPressed(downKey) && paddle.Position.Y + paddle.Size.Y < WindowHeight)
        {
            paddle.Position += new Vector2f(0, 5);
        }
    }


    private void Draw()
    {
        window.Clear(Color.Black);
        window.Draw(player1Paddle);
        window.Draw(player2Paddle);
        window.Draw(ball.Shape);
        scoreboard.Draw(window);
        window.Display();
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Escape)
        {
            window.Close();
        }
    }
}