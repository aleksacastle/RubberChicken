using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Rubber_Chicken
{
    /// <summary>
    /// A Rubber Chicken class
    /// </summary>
    class RubberChicken
    {
        #region Fields

        bool active = true;
        int damage;

        //drawing and moving  support
        Texture2D sprite;
        Rectangle drawRectangle;

        //moving support
        const int RUBBER_CHICKEN_SPEED = 5;
        Vector2 velocity = Vector2.Zero;

        //click processing support

        bool clickStarted = false;
        bool buttonReleased = true;
        bool moving = false;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="location">location of center</param>
        /// <param name="damage">damage from rubber chicken</param>
        public RubberChicken(Texture2D sprite, Vector2 location,
            int damage)
        {
            this.sprite = sprite;
            this.damage = damage;

            //build draw rectangle
            drawRectangle = new Rectangle(
                (int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2,
                sprite.Width, sprite.Height);

        }

        #endregion
        #region Properties

        /// <summary>
        /// Get and sets whether the rubber chicken is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Collision Rectangle for the rubber chicken
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }
        /// <summary>
        /// Gets damage from rubber chicken
        /// </summary>
        public int Damage
        {
            get { return damage; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// updates rubber chicken, moving and launching
        /// when clicked
        /// </summary>
        /// <param name="gameTime">game Time</param>
        /// <param name="mouse">mouse state</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
            //move based on the velocity
            drawRectangle.X += (int)velocity.X * gameTime.ElapsedGameTime.Milliseconds;
            drawRectangle.Y += (int)velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

            //launch on click
            // check for mouse over rubber chicken
            if (drawRectangle.Contains(mouse.X, mouse.Y))
            {
                // check for click started on rubber chicken
                if (mouse.LeftButton == ButtonState.Pressed &&
                    buttonReleased)
                {
                    clickStarted = true;
                    buttonReleased = false;
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    buttonReleased = true;

                    // if click finished on rubber chicken, launch as appropriate
                    if (clickStarted)
                    {
                        // launch if not already moving
                        if (!moving)
                        {
                            moving = true;
                            velocity = new Vector2(0, - RUBBER_CHICKEN_SPEED);
                        }

                        clickStarted = false;
                    }
                }
            }
        }
        /// <summary>
        /// draw rubber chicken
        /// </summary>
        /// <param name="spriteBatch">sprite</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,drawRectangle,Color.White);
        }
        #endregion
    }
}
