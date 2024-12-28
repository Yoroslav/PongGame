using SFML.Graphics;
using SFML.System;

class Scoreboard
{
    private Text scoreText;
    private int player1Score = 0;
    private int player2Score = 0;

    public Scoreboard(Font font, Vector2f position)
    {
        scoreText = new Text("0 : 0", font, 24)
        {
            FillColor = Color.White,
            Position = position
        };
    }

    public void UpdateScore(int player1, int player2)
    {
        player1Score = player1;
        player2Score = player2;
        scoreText.DisplayedString = $"{player1Score} : {player2Score}";
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(scoreText);
    }
}