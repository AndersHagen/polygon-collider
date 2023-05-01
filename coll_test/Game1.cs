using System.Collections.Generic;
using coll_test.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coll_test;

public class Game1 : Game
{
    private Vector2 _playerOrigin;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private int _playerX;
    private int _playerY;
    private float _rot;
    private Color _playerColor;

    private CollisionBound _evilRectangle;
    private CollisionBound _playerBound;

    private CollisionHandler _handler;

    private Texture2D _texture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
        _texture.SetData(new Color[] { Color.White });

        _playerX = 200;
        _playerY = 100;
        _rot = 0f;
        _playerColor = Color.Green;

        _playerOrigin = new Vector2(0.5f, 0.5f);

        _playerBound = new CollisionBound(
            _playerX,
            _playerY,
            new List<Vector2>
            {
                new Vector2(-25, -25),
                new Vector2(25, -25),
                new Vector2(25, 25),
                new Vector2(-25, 25)
            });

        _evilRectangle = new CollisionBound(
            400,
            300,
            new List<Vector2>
            {
                new Vector2(-50, -100),
                new Vector2(75, -100),
                new Vector2(75, 130),
                new Vector2(-50, 130)
            });

        _handler = new CollisionHandler();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var keys = Keyboard.GetState();

        if (keys.IsKeyDown(Keys.Up))
        {
            _playerY--;
        }
        if (keys.IsKeyDown(Keys.Down))
        {
            _playerY++;
        }
        if (keys.IsKeyDown(Keys.Left))
        {
            if (keys.IsKeyDown(Keys.LeftShift))
            {
                _rot += 0.02f;
            }
            else
            {
                _playerX--;
            }
        }
        if (keys.IsKeyDown(Keys.Right))
        {
            if (keys.IsKeyDown(Keys.LeftShift))
            {
                _rot -= 0.02f;
            }
            else
            {
                _playerX++;
            }
        }

        _playerBound.SetCenter(_playerX, _playerY);
        _playerBound.Rotate(_rot);

        _playerColor = _handler.Overlaps(_playerBound, _evilRectangle) ? Color.GreenYellow : Color.Green;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        var vp = _playerBound.VerticesXY();
        var ve = _evilRectangle.VerticesXY();

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _spriteBatch.Draw(
            _texture,
            new Rectangle(_playerX, _playerY, 50, 50),
            null,
            _playerColor,
            _rot,
            _playerOrigin,
            SpriteEffects.None,
            0f
            );

        _spriteBatch.Draw(_texture, new Rectangle(350, 200, 125, 230), Color.Red);

        foreach (var v in vp)
        {
            _spriteBatch.Draw(_texture, v, Color.LightYellow);
        }

        foreach (var v in ve)
        {
            _spriteBatch.Draw(_texture, v, Color.LightYellow);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

