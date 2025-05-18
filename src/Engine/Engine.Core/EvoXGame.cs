using EvoX.Engine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EvoX.Engine.Core;

/// <summary>
/// Abstract base class for a MonoGame framework-based game.
/// </summary>
public abstract class EvoXGame : Game
{
    // Private fields
    private RenderTarget2D? _offScreenRenderTarget;
    private float? _aspectRatio;
    private Point? _oldWindowSize;

    // Constructor
    public EvoXGame()
    {
        Graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnResize;
    }

    // Constructor with graphics scale parameter
    public EvoXGame(int graphicsScale)
        : this()
    {
        GraphicsScale = graphicsScale;
    }

    // Static properties
    /// <summary>
    /// Gets or sets the graphics scale factor.
    /// </summary>
    public static int GraphicsScale { get; set; } = 1;

    /// <summary>
    /// Gets or sets whether the game is paused.
    /// </summary>
    public static bool GameIsPaused { get; set; }

    /// <summary>
    /// Gets or sets the array of game states.
    /// </summary>
    public static IGameState[] GameStates { get; set; }

    // Protected properties
    /// <summary>
    /// Gets the SpriteBatch used for rendering.
    /// </summary>
    protected SpriteBatch? SpriteBatch { get; private set; }

    /// <summary>
    /// Gets the GraphicsDeviceManager for managing graphics settings.
    /// </summary>
    protected GraphicsDeviceManager Graphics { get; }

    // Protected methods

    /// <summary>
    /// Initializes game services required for the game.
    /// </summary>
    /// <param name="tmxMapsInfo">List of map information objects.</param>
    /// <param name="userControls">Dictionary of user control mappings.</param>
    //protected void InitializeGameServices(List<MapInfo> tmxMapsInfo, Dictionary<string, InputTypes> userControls)
    //{
    //    // Initialize level manager with provided TMX maps information
    //    LevelManager.Init(Content, tmxMapsInfo);

    //    // Initialize input manager with the current window size
    //    InputManager.Init(new System.Drawing.Size(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight));

    //    // Set user control mappings
    //    InputManager.UserControls = userControls;

    //    // Add content manager service
    //    Services.AddService(Content);
    //}

    /// <summary>
    /// Updates the game logic.
    /// </summary>
    /// <param name="gameTime">Snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        // Update game logic only if game is not paused
        if (!GameIsPaused)
        {
           // LevelManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        // Update input manager with current window bounds
       // InputManager.Update(Window.ClientBounds);

        // Call base update method
        base.Update(gameTime);
    }

    /// <summary>
    /// Loads game content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch for rendering sprites
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // Store aspect ratio of the viewport
        _aspectRatio = GraphicsDevice.Viewport.AspectRatio;

        // Store initial window size
        _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

        // Create an off-screen render target for post-processing
        _offScreenRenderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
    }

    /// <summary>
    /// Begins drawing the game scene.
    /// </summary>
    /// <returns>True if drawing can begin, false otherwise.</returns>
    protected override bool BeginDraw()
    {
        // Set render target to off-screen render target
        GraphicsDevice.SetRenderTarget(_offScreenRenderTarget);

        // Call base begin draw method
        return base.BeginDraw();
    }

    /// <summary>
    /// Ends drawing the game scene.
    /// </summary>
    protected override void EndDraw()
    {
        // Reset render target to null (back buffer)
        GraphicsDevice.SetRenderTarget(null);

        if(SpriteBatch is not null)
        {
            // Begin sprite batch drawing
            SpriteBatch.Begin();

            // Draw the off-screen render target to the screen
            SpriteBatch.Draw(_offScreenRenderTarget, GraphicsDevice.Viewport.Bounds, Color.White);

            // End sprite batch drawing
            SpriteBatch.End();
        }     

        // Call base end draw method
        base.EndDraw();
    }

    /// <summary>
    /// Disposes of resources used by the game.
    /// </summary>
    /// <param name="disposing">True if disposing managed resources, false otherwise.</param>
    protected override void Dispose(bool disposing)
    {
        // Dispose off-screen render target
        if(_offScreenRenderTarget != null) 
            _offScreenRenderTarget.Dispose();

        // Call base dispose method
        base.Dispose(disposing);
    }

    // Private methods

    /// <summary>
    /// Handles the window resize event.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    private void OnResize(object? sender, EventArgs e)
    {
        if (_oldWindowSize == null || _aspectRatio == null)
            return;
        // Remove this event handler temporarily to prevent recursive calls
        Window.ClientSizeChanged -= OnResize;

        // Check if width has changed
        if (Window.ClientBounds.Width != _oldWindowSize.Value.X)
        {
            // Adjust preferred back buffer width
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;

            // Adjust preferred back buffer height to maintain aspect ratio
            Graphics.PreferredBackBufferHeight = (int)(Window.ClientBounds.Width / _aspectRatio);
        }

        // Check if height has changed
        if (Window.ClientBounds.Height != _oldWindowSize.Value.Y)
        {
            // Adjust preferred back buffer width to maintain aspect ratio
            Graphics.PreferredBackBufferWidth = (int)(Window.ClientBounds.Height * _aspectRatio);

            // Adjust preferred back buffer height
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        }

        // Apply graphics changes
        Graphics.ApplyChanges();

        // Update old window size with current dimensions
        _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

        // Re-attach the event handler
        Window.ClientSizeChanged += OnResize;
    }
}
