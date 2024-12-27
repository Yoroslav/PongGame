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
    private CircleShape ball;

    private Vector2f ballVelocity;
    private int player1Score = 0;
    private int player2Score = 0;

    private Font font;
    private Text scoreText;

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

        ball = new CircleShape(10)
        {
            FillColor = Color.White,
            Position = new Vector2f(WindowWidth / 2, WindowHeight / 2)
        };

        ballVelocity = new Vector2f(-4, -4);

        font = new Font(@"C:\Users\Yaroslav\OneDrive\PongGame\Game\OpenSans.ttf");
        scoreText = new Text("0 : 0", font, 24)
        {
            FillColor = Color.White,
            Position = new Vector2f(WindowWidth / 2 - 30, 10)
        };
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
        ball.Position += ballVelocity;

        if (ball.Position.Y <= 0 || ball.Position.Y + ball.Radius * 2 >= WindowHeight)
        {
            ballVelocity.Y = -ballVelocity.Y;
        }

        if (ball.GetGlobalBounds().Intersects(player1Paddle.GetGlobalBounds()) ||
            ball.GetGlobalBounds().Intersects(player2Paddle.GetGlobalBounds()))
        {
            ballVelocity.X = -ballVelocity.X;
        }

        if (ball.Position.X <= 0)
        {
            player2Score++;
            ResetBall();
        }
        else if (ball.Position.X + ball.Radius * 2 >= WindowWidth)
        {
            player1Score++;
            ResetBall();
        }

        scoreText.DisplayedString = $"{player1Score} : {player2Score}";

        if (Keyboard.IsKeyPressed(Keyboard.Key.W) && player1Paddle.Position.Y > 0)
        {
            player1Paddle.Position += new Vector2f(0, -5);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.S) && player1Paddle.Position.Y + player1Paddle.Size.Y < WindowHeight)
        {
            player1Paddle.Position += new Vector2f(0, 5);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && player2Paddle.Position.Y > 0)
        {
            player2Paddle.Position += new Vector2f(0, -5);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && player2Paddle.Position.Y + player2Paddle.Size.Y < WindowHeight)
        {
            player2Paddle.Position += new Vector2f(0, 5);
        }
    }

    private void Draw()
    {
        window.Clear(Color.Black);
        window.Draw(player1Paddle);
        window.Draw(player2Paddle);
        window.Draw(ball);
        window.Draw(scoreText);
        window.Display();
    }

    private void ResetBall()
    {
        ball.Position = new Vector2f(WindowWidth / 2, WindowHeight / 2);
        ballVelocity = new Vector2f(-ballVelocity.X, -4);
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Escape)
        {
            window.Close();
        }
    }
}
