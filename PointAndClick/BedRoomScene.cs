﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace PointAndClick
{
    public class BedRoomScene : SceneScreen
    {
        private Song music;
        private SoundEffect cosmic;
        private SoundEffect chop;
        private BackGround background;
        private Conversation Introduction;
        private Texture2D princessTexture;
        private Texture2D fishTexture;
        private Texture2D heroIcon;
        private Hero hero;
        private Princess princess;
        private Item socket;
        private Fish fish;
        private Item pottedPlant;
        private Item princessHand;
        private Cat kitty;
        public bool fishPotted;
        public bool princessFed;
        private ArrowButton arrowUp;
        private ArrowButton arrowDown;
        private ArrowButton arrowRight;
        private ArrowButton arrowLeft;

        bool introduced;

        public BedRoomScene(MainGame game)
            :base(game)
        {
            introduced = false;
            fishPotted = false;
            princessFed = false;

        }

        public override void LoadContent()
        {
             
            background = new BackGround(new Vector2(0, 0), "Backgrounds/bedroom", mainGame);
            arrowLeft = new ArrowButton(new Vector2(50, 120), "Objects/arrowLeft", mainGame, GameStates.Kitchen);
            arrowRight = new ArrowButton(new Vector2(250, 120), "Objects/arrowRight", mainGame, GameStates.MarketBack);   // remove later

            Introduction = new Conversation();
            princessTexture = mainGame.Content.Load<Texture2D>(@"Icons\bedroomPrincessWhateverIcon");
            fishTexture = mainGame.Content.Load<Texture2D>(@"Icons\bedroom-magiKoyHealthyIcon");
            heroIcon = mainGame.Content.Load<Texture2D>(@"Icons\heroIcon");
            socket = new Item(new Vector2(965, 440), @"Objects\bedroom-socket", mainGame, "", false);
            pottedPlant = new Item(new Vector2(425, 140), @"Objects\bedroom-pottedPlant", mainGame, @"Icons\inv-pottedPlantIcon", true);
            princessHand = new Item(new Vector2(1100, 725), @"Objects\bedroom-princessWhateverHand", mainGame, @"Icons\inv-princessWhateverHandIcon", true);
            hero = new Hero(mainGame, heroIcon);
            kitty = new Cat(mainGame, heroIcon);

            princess = new Princess(mainGame, heroIcon);
            fish = new Fish(mainGame, heroIcon, princess, this);


            drawingList.Add(background);
            AddObject(pottedPlant);
            AddObject(fish);
            AddObject(socket);
            AddObject(princess);
            AddObject(hero);
            AddObject(arrowLeft);
            Introduction.Addline(new Tuple<Texture2D,Texture2D,string,string>(heroIcon,
                                                                            princess.examineTexture,
                                                                            "ZzZzzZzz... Wtf is this shit!", 
                                                                            "A wild MAGIKOI appeared!"
                                                                            ));
           
            mainGame.iMenu.StartConversation(Introduction);
            cosmic = mainGame.Content.Load<SoundEffect>(@"SFX\cosmic");
            cosmic.Play();
                
        }

        public override void Update(GameTime gametime)
        {   

            if(!introduced)
            {   
                introduced = true;
                hero.UpdateHeroState(HeroState.Awake);

            }

            if(fishPotted)
            {
                chop = mainGame.Content.Load<SoundEffect>(@"SFX\augh");
                chop.Play();
                AddObject(princessHand);
                princess.UpdatePrincessState(PrincessState.Injured);
                mainGame.iMenu.DiscardItem();
                fishPotted = false;
            }

            if (princessFed)
            {
          
                cosmic.Play();
                
 
                if(princess.state == PrincessState.Disgusted)
                {
                    princess.visible = false;
                    objectList.RemoveAll((item => item is Princess));

                }
                    

                AddObject(kitty);
                mainGame.iMenu.DiscardItem();
                princessFed = false;
       
            }

            base.Update(gametime);

        }

    }
}