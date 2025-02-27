﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace PointAndClick
{
    //Class for any image draw to screen with a texture
    public class SceneImage : Drawable
    {

        protected Vector2 size;
        public Texture2D currentTexture { get; protected set; }
        public Texture2D initialTexture { get; protected set; }
        protected Rectangle drawRectangle;

        public SceneImage(Vector2 initPosition, String path, MainGame currentGame)
            : base(initPosition, currentGame, path)
        {
            initialTexture = currentGame.Content.Load<Texture2D>(path);
            size = new Vector2(initialTexture.Width , initialTexture.Height);
            drawRectangle = new Rectangle((int)(position.X * maingame.ScalingFactor.X),
                                          (int)(position.Y * maingame.ScalingFactor.Y),
                                          (int)(size.X * maingame.ScalingFactor.X),
                                          (int)(size.Y * maingame.ScalingFactor.Y));

            currentTexture = initialTexture;
        }

        public SceneImage(Vector2 initPosition, MainGame currentGame)
            : base(initPosition, currentGame, "")
        {
           
        }
        
        //Method to draw image
        public override void Draw()
        {   
            if(visible)
                maingame.spriteBatch.Draw(currentTexture, 
                                          new Vector2(position.X * maingame.ScalingFactor.X, position.Y * maingame.ScalingFactor.Y),
                                          null, 
                                          Color.White, 
                                          0,
                                          new Vector2(0, 0), 
                                          maingame.ScalingFactor, 
                                          SpriteEffects.None, 
                                          0);
           
        }

        //Method to update position on screen
        public override void UpdatePosition(Vector2 newPosition)
        {   
            
            base.UpdatePosition(newPosition);

            drawRectangle = new Rectangle((int)(position.X * maingame.ScalingFactor.X),
                                          (int)(position.Y * maingame.ScalingFactor.Y),
                                          (int)(size.X * maingame.ScalingFactor.X),
                                          (int)(size.Y * maingame.ScalingFactor.Y));
 
        }

        public override void TranitionDraw(int mAlphaValue)
        {

            if (visible)
                maingame.spriteBatch.Draw(currentTexture,
                                          new Vector2(position.X * maingame.ScalingFactor.X, position.Y * maingame.ScalingFactor.Y),
                                          null,
                                          new Color(255, 255, 255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)),
                                          0,
                                          new Vector2(0, 0),
                                          maingame.ScalingFactor,
                                          SpriteEffects.None,
                                          0);

        }

        public void UpdateCurrentTexture(Texture2D newTexture)
        {

            currentTexture = newTexture;
            UpdateSizeAndRec();
        }

        public void ResetTexture()
        {
            currentTexture = initialTexture;
            UpdateSizeAndRec();
        }

        private void UpdateSizeAndRec()
        {
            size = new Vector2(currentTexture.Width, currentTexture.Height);
            drawRectangle = new Rectangle((int)(position.X * maingame.ScalingFactor.X),
                                          (int)(position.Y * maingame.ScalingFactor.Y),
                                          (int)(size.X * maingame.ScalingFactor.X),
                                          (int)(size.Y * maingame.ScalingFactor.Y));
        }

    }

}
