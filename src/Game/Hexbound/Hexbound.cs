using EvoX.Engine.Common;
using EvoX.Engine.Core;
using EvoX.Hexbound.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvoX.Hexbound;
public class Hexbound : EvoXGame
{
    public static bool DoExitGame { get; set; }

    public Hexbound() : base(graphicsScale: 4)
    {
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.ApplyChanges();


        GameStates = new IGameState[]
       {
                new GameController(Services) { IsActive = true },
       };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (DoExitGame)
        {
            Exit();
        }

        foreach (var activeState in GameStates.Where(x => x.IsActive))
        {
            activeState.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(0, 16, 33));
        Matrix transform = Matrix.CreateScale(GraphicsScale);

        SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, transform);

        foreach (var activeState in GameStates.Where(x => x.IsActive))
        {
            activeState.Draw(gameTime, SpriteBatch);
        }

        SpriteBatch.End();

        if (!GameIsPaused)
        {
            var gameController = GameStates.Where(x => x is GameController && x.IsActive).FirstOrDefault();

            if (gameController != null && !GameIsPaused)
            {
                SpriteBatch.Begin();
                (gameController as GameController).DrawDebugPanel(gameTime, SpriteBatch);
                SpriteBatch.End();
            }
        }

        base.Draw(gameTime);
    }
}
