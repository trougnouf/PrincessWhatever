﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace PointAndClick
{
    public class Item : ClickableObject
    {


        public bool inScene { get; private set; }
        public String description { get; protected set; }
        protected Texture2D inBagTexture;
        public bool takeable { get; private set; }
        public Texture2D examineTexture { get; protected set; }
        protected SoundEffect effect;

        public Item(Vector2 initPosition, String path, MainGame currentGame, string bagTexture, bool istakable, string newExamineTexture)
            : this(initPosition, path, currentGame, bagTexture, istakable)
        {
            examineTexture = currentGame.Content.Load<Texture2D>(newExamineTexture);
        }

        public Item(Vector2 initPosition, String path, MainGame currentGame, string bagTexture, bool istakable) 
            :base(initPosition, path, currentGame)
        {

            takeable = istakable;
            inScene = true;
            if(takeable)
            inBagTexture = currentGame.Content.Load<Texture2D>(bagTexture);

            if(!(this is Character))
            {
                examineTexture = initialTexture;
            }

            //Fills in Item descriptions based on path
            switch(path)
            {   
                case @"Objects\bedroom-pottedPlant":
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\thud");
                    description = "This smells like probable cause.";
                    break;

                case @"Objects\bedroom-magikoiHealthy":
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\koi");
                    description = "This magikoi is frantically flapping its fins.";
                    break;

                case @"Objects\bedroom-magikoiDead":

                    description = "A meal this foul is sure to ravage the bowels.";
                    break;


                case @"Objects\bedroom-magikoiPotted":

                    description = "The magikoi has been potted!";
                    break;

                case @"Objects\bedroom-princessWhateverHand":

                    description = "The princess's severed hand...";
                    break;

                case @"Objects\bedroom-socket":

                    description = "This Danish socket looks oddly happy.";
                    break;

                case @"Objects\bedroom-princessWhateverHealthy":
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\hello");
                    description = "The alluring Princess Whatever.";
                    break;

                case @"Objects\heroLaying":

                    description = "It's me! I'm a Penguin...";
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\rawr");
                    break;

                case @"Objects\bedroom-cat":

                    description = "A mystical fluffy cat.... he's so mysterious.";
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\kitten");
                    break;

                case @"Objects\parking-jumperCables":

                    description = "Is that a snake?!... No, it just looks like some jumper cables.";
                    break;

                case @"Objects\parking-cockMobile":

                    description = "Woah that is a large chicken!";
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\rooster");

                    break;

                case @"Objects\bank-tellerBackground":
                    description = "A bank teller.";
                    effect = maingame.Content.Load<SoundEffect>(@"SFX\zoop");
                    break;

                case @"Objects\groceryStoreBack-baconPackBackground":

                    description = "Wow... Look at all that bacon!!!!!!!";
                    break;

                case @"Objects\kitchen-jackHammer":

                    description = "Looks like a fun destructive toy.";
                    break;

                case @"Objects\kitchen-mail":

                    description = "A letter from a Nigerian Prince offering me an opportunity. Seems fishy...";
                    break;

                case @"Objects\kitchen-onLight":

                    description = "Better not touch it while it is on!";
                    break;

                case @"Objects\kitchen-pan":

                    description = " A simple pan, that might be useful for cooking delicious bacon!";
                    break;

                case @"Objects\kitchen-panWithBacon":

                    description = "Wow... Look at all that bacon!!!!!!!";
                    break;

                case @"Objects\kitchen-stoveTop":

                    description = "Hmmm... It doesn't seem to be cooking";
                    break;

                case @"Objects\bank-creditCard":

                    description = "A credit card... With no credit limit!!!";
                    break;

                case @"Objects\kitchen-rawBaconPlate":

                    description = "Eww! It's so slimy!";
                    break;

                case @"Objects\kitchen-perfectBaconPlate":

                    description = "Delicious, crispy bacon. Food of the gods.";
                    break;

                case @"Objects\kitchen-burnedBaconPlate":

                    description = "The charred remains of what used to be delicious bacon.";
                    break;

                default:
                    description = "";
                    break;
            }

        }

        public void PlayEffect()
        {   
            if(effect != null)
            effect.Play();
        }
 
        public void ChangeToBagTexture()
        {   
            inScene = false;
            currentTexture = inBagTexture;
        }

        public void PutBAckInScene()
        {
            inScene = true;
            ResetTexture();
        }

        protected override void OnClick(GameStates state)
        {
            if (maingame.iMenu.currentItem != null)
            {
                if (path == @"Objects\groceryStore-creditCardTerminalBackground" && maingame.iMenu.currentItem.path == @"Objects\bank-creditCard" && maingame.iMenu.usingItem)
                    ((MarketScene)maingame.currentScreen).PayedFor();
                else
                    maingame.iMenu.Options(this);
                
            }
            else
            maingame.iMenu.Options(this);
           
       
        }
        protected override void OnUnClick(GameStates state)
        {
            
        }
        protected override void OnMouseEnter(GameStates state)
        {

        }
        protected override void OnMouseLeave(GameStates state)
        {

        }

        public override void Draw()
        {
            if (visible)
            {

                if (IsMouseOver && inScene)
                    maingame.spriteBatch.Draw(currentTexture,
                                              new Vector2(position.X * maingame.ScalingFactor.X, position.Y * maingame.ScalingFactor.Y),
                                              null,
                                              Color.White,
                                              0,
                                              new Vector2(0, 0),
                                              new Vector2((float)(maingame.ScalingFactor.X * 1.2), (float)(maingame.ScalingFactor.Y * 1.2)),
                                              SpriteEffects.None,
                                              0);
                else if (IsMouseOver && !inScene)
                    maingame.spriteBatch.Draw(currentTexture,
                                              new Vector2(position.X * maingame.ScalingFactor.X, position.Y * maingame.ScalingFactor.Y),
                                              null,
                                              Color.White,
                                              0,
                                              new Vector2(0, 0),
                                              new Vector2((float)(maingame.ScalingFactor.X * 1.1), (float)(maingame.ScalingFactor.Y * 1.1)),
                                              SpriteEffects.None,
                                              0);
                //maingame.spriteBatch.Draw(texture, drawRectangle, Color.Aqua);
                else
                    base.Draw();
            }
       }
            
    }
}
