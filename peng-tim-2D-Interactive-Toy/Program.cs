namespace peng_tim_2D_Interactive_Toy;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;
using System;

public class Program
{
    const string title = "Rainbow Circles";
    const int width = 800;
    const int height = 600;
    const float circleLifetime = 1.0f;  // circle lifetime (unsure if it's in accurately in seconds)

    struct Circle
    {
        public Vector2 Position;
        public Color Color;
        public float CreationTime;
        public float Size;
    }

    static List<Circle> circles = new List<Circle>();

    static Color[] rainbowColors = {
        new Color(255, 0, 0, 255),    // red
        new Color(255, 165, 0, 255),  // orange
        new Color(255, 255, 0, 255),  // yellow
        new Color(0, 128, 0, 255),    // green
        new Color(0, 0, 255, 255),    // blue
        new Color(128, 0, 128, 255),  // purple
        new Color(238, 130, 238, 255) // violet
    };

    static void Main(string[] args)
    {
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            float currentTime = (float)Raylib.GetTime();

            // check if the left mouse button is clicked/down
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                Vector2 position = Raylib.GetMousePosition();
                Random random = new Random();
                Color color = rainbowColors[random.Next(rainbowColors.Length)];

                circles.Add(new Circle
                {
                    Position = position,
                    Color = color,
                    CreationTime = currentTime,
                    Size = 50
                });
            }

            // update the circles
            for (int i = circles.Count - 1; i >= 0; i--)
            {
                Circle circle = circles[i];
                float elapsedTime = currentTime - circle.CreationTime;
                float shrinkRate = circle.Size / circleLifetime; // Size decrease per second
                circle.Size -= shrinkRate * Raylib.GetFrameTime();

                // remove the circles that have shrunken to their smallest size
                if (circle.Size <= 0)
                {
                    circles.RemoveAt(i);
                }
                else
                {
                    circles[i] = circle; // and then update the circles back into the list
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            // draw the circles
            foreach (var circle in circles)
            {
                Raylib.DrawCircleV(circle.Position, circle.Size, circle.Color);
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
