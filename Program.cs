using SFML;
using SFML.Graphics;
// using static SFML.Graphics.Color;
using SFML.Window;
using SFML.System;
using static SFML.Graphics.Color;

namespace App
{
    class Program
    {
        static bool isEdown = false;
        delegate void Del();
        static void Main(string[] args)
        {
            const int WIDTH = 1000;
            const int HEIGHT = 600;
            const string TITLE = "SHRUMP";

            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE);
            View view = new View(new FloatRect(0.0f, 0.0f, 1000f, 600f));
            window.SetVerticalSyncEnabled(true);

            window.Closed += (sender, args) => window.Close();
            window.KeyReleased += HandleKeyUp;

            Player player = new Player(40, WIDTH/2-50, new Texture("assets/player0.png"));
            player.playerSprite.Scale = new Vector2f(3f, 3f);
            bool isGrounded = false;
            float acceleration = 0.2f;
            float velocity = 0f;

            int animationTimer = 0;
            int dl = 0;
            
            RectangleShape ground = new RectangleShape()
            {
                Size = new Vector2f(1000, 100),
                FillColor = new Color(110, 110, 110),
                Position = new Vector2f(0, 500)
            };
            player.posY = 447+player.playerSprite.Scale.Y - 100;
            player.posX = WIDTH/2-50;

            Mushroom mushroom = new Mushroom(700f, 0f, new Texture("assets/mushroom.png"));
            mushroom.mushroomSprite.Position = new Vector2f(700f, 447+mushroom.mushroomSprite.Scale.Y);
            bool bounced = false;
            float bounciness = 10;

            Color[] colors = {
                Red,
                Yellow,
                Green,
                Blue,
                Magenta,
                White
            };
            RectangleShape selectRect = new RectangleShape()
            {
                Size = new Vector2f(50f, 50f),
            };
        
            Sprite enemy = new Sprite()
            {
                Texture = new Texture("assets/enemy0.png"),
            };

            Text instructions = new Text()
            {
                DisplayedString = "Press E to change the player colour",
                CharacterSize = 20,
                FillColor = White,
                Position = new Vector2f(20f, 20f),
                Font = new Font("font.ttf")
            };

            float fps = 0;
            Text fpsLabel = new Text()
            {
                DisplayedString = $"FPS: {fps}",
                CharacterSize = 20,
                FillColor = White,
                Position = new Vector2f(400f, 20f),
                Font = new Font("font.ttf")
            };

            Clock clock = new Clock();
            float lastTime = 0;
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                    window.Clear(Black);
                    window.Draw(instructions);

                    float currentTime = clock.Restart().AsSeconds();
                    fps = 1.0f / currentTime;
                    lastTime = currentTime;
                    fpsLabel.DisplayedString = $"FPS: {(int)fps}";
                    window.Draw(fpsLabel);
                    Console.WriteLine($"X: {player.playerSprite.Position.X}, Y: {player.playerSprite.Position.Y}");
                    
                    //Console.WriteLine($"{isGrounded}, {player.posX}, {player.posY}, {bounced}");

                    // if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    // {
                    //     if (Utils.CheckCollisions(Mouse.GetPosition().X, Mouse.GetPosition().Y, 5, 5, 50 + 0 * 80, 100f, 50, 50))
                    //     {
                    //         selectRect.OutlineColor = Color.White;

                    //     }
                    // }

                    // for (int x = 0; x < colors.Length; x++)
                    // {
                    //     selectRect.Position = new Vector2f(50 + x * 80, 100f);
                    //     selectRect.FillColor = colors[x];
                    //     window.Draw(selectRect);
                    // }


                    if (isGrounded == false)
                    {
                        velocity += acceleration;
                        player.posY += velocity;
                        player.playerSprite.Texture = new Texture("assets/player5.png");
                    }
                    else
                    {
                        velocity = 0;
                    }
                    if (Utils.CheckCollisions(player.posX, player.posY, 50, 50, 0, 500, 1000, 100))
                    {
                        if (isGrounded == false)
                        {
                            isGrounded = true;
                            player.posY = 447+player.playerSprite.Scale.Y;
                        }
                    }
                    //bounced = !isGrounded;
                    //if (Utils.CheckCollisions(player.posX, player.posY, 50, 50, mushroom.mushroomSprite.Position.X, mushroom.mushroomSprite.Position.Y, mushroom.mushroomSprite.Scale.X, mushroom.mushroomSprite.Scale.Y))
                    //if (Utils.CheckCollisionsY(player.posY, 50, mushroom.mushroomSprite.Position.Y, 50))
                    //if (Utils.CheckCollisionsX(player.posX, 50, mushroom.mushroomSprite.Position.X, 50))
                    if (Utils.CheckCollisions(player.posX, player.posY, 50, 50, mushroom.mushroomSprite.Position.X, mushroom.mushroomSprite.Position.Y, 25, mushroom.mushroomSprite.Scale.Y)) 
                    {
                        if (bounced == false && player.posY >= 300)
                        {
                            //bounced = true;
                            if (isGrounded == false)
                            {
                                velocity = 0;
                                velocity -= bounciness;

                                if (bounciness < 13)
                                    bounciness++;
                                else
                                    bounciness--;
                            }
                        }
                        // else
                        // {
                        //     if (player.posX >= 650)
                        //     {
                        //         player.posX = 650;
                        //     }
                        //     else if (player.posX <= 800)
                        //     {
                        //         player.posX = 800;
                        //     }
                        // }
                    }
                    if (Utils.UnCollide(player.posX, player.posY, 50, 50, mushroom.mushroomSprite.Position.X, mushroom.mushroomSprite.Position.Y, 50, mushroom.mushroomSprite.Scale.Y))
                    {
                        bounced = false;
                    }
                    if (Utils.UnCollide(player.posX, player.posY, 50, 50, 0, 500, 1000, 100))
                    {
                        Console.WriteLine("hi");
                        isGrounded = false;
                        velocity = 0.2f;                  
                    }
                    if (player.posX >= 1000)
                    {
                        isGrounded = false;
                    }

                    window.SetView(view);
                    window.Draw(ground);
                    window.Draw(mushroom.mushroomSprite);
                    
                    if (isGrounded == true)
                    {
                        if (animationTimer < 6)
                            player.playerSprite.Texture = new Texture($"assets/player{animationTimer}.png");
                        else
                            player.playerSprite.Texture = new Texture($"assets/player6.png");
                    }

                    window.Draw(player.playerSprite);

                    player.playerSprite.Position = new Vector2f(player.posX, player.posY);

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    {
                        player.playerSprite.Scale = new Vector2f(-3, 3f);
                        player.posX -= 3;
                        view.Move(new Vector2f(-3f, 0f));
                        if (dl < 4)
                        {
                            dl++;
                            if (animationTimer == 4)
                                animationTimer = 1;
                        }
                        else
                        {
                            dl = 0;
                            animationTimer++;
                        }
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    {
                        player.playerSprite.Scale = new Vector2f(3, 3f);
                        player.posX += 3;
                        view.Move(new Vector2f(3f, 0f));
                        if (dl < 4)
                        {
                            dl++;
                            if (animationTimer == 4)
                                animationTimer = 1;
                        }
                        else
                        {
                            dl = 0;
                            animationTimer++;
                        }
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        if (isGrounded == true)
                        {
                            velocity -= 5f;
                            isGrounded = false;
                        }
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                    {
                        // Random rc = new Random();
                        // int R = rc.Next(0, 255);
                        // int G = rc.Next(0, 255);
                        // int B = rc.Next(0, 255);
                        if (isEdown == false)
                        {
                            isEdown = true;
                            player.playerSprite.Color = colors[new Random().Next(0, colors.Length-1)];//new Color((byte)R, (byte)G, (byte)B);colors[new Random().Next(0, colors.Length-1)];
                        }
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                    {
                        animationTimer = 6;
                    }

                    // if (!Keyboard.IsKeyPressed(Keyboard.Key.A))
                    // {
                    //     dl = 0;
                    //     animationTimer = 0;
                    // }
                    // else if (!Keyboard.IsKeyPressed(Keyboard.Key.D))
                    // {
                    //     dl = 0;
                    //     animationTimer = 0;
                    // }

                    
                window.Display();
            }

            void HandleKeyUp(object sender, KeyEventArgs e)
            {
                if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.D)
                {
                    dl = 0;
                    animationTimer = 0;
                }
                if (e.Code == Keyboard.Key.E)
                {
                    if (isEdown)
                        isEdown = false;
                }
                
            }
        }

    }
}