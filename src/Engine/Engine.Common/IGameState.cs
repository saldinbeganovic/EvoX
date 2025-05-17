using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvoX.Engine.Common;

/// <summary>
/// Interface defining a game state in the game loop.
/// </summary>
public interface IGameState
{
    /// <summary>
    /// Gets or sets whether the game state is active.
    /// </summary>
    bool IsActive { get; set; }

    /// <summary>
    /// Updates the game state logic.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    void Update(GameTime gameTime);

    /// <summary>
    /// Executes post-update logic after the main update.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    void PostUpdate(GameTime gameTime);

    /// <summary>
    /// Draws the game state.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
