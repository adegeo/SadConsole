﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = SadConsole.Consoles.Console;
using SadConsole.Entities;
using SadConsole.Consoles;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarterProject.CustomConsoles
{
    class EntityAndConsole: Console
    {
        // The console here acts like a playing field for our entities. You could draw some sort of area for the
        // entity to walk around on. The console also gets focused with the keyboard and accepts keyboard events.

        private Player _player;
        private Point _playerPreviousPosition;

        public EntityAndConsole()
            : base(80, 25)
        {
            // Create out player entity, and center it.
            _player = new Player();
            _player.Position = new Microsoft.Xna.Framework.Point(_textSurface.Width / 2, _textSurface.Height / 2);
            _playerPreviousPosition = _player.Position;

            // Setup this console to accept keyboard input.
            CanUseKeyboard = true;
            IsVisible = false;
        }

        public override void Update()
        {
            // Update the entities.
            _player.Update();

            base.Update();
        }

        public override void Render()
        {
            if (IsVisible)
            {
                base.Render();

                _player.Render();
            }
        }

        public override bool ProcessKeyboard(SadConsole.Input.KeyboardInfo info)
        {
            // Forward the keyboard data to the entity to handle the movement code.
            // We could detect if the users hit ESC and popup a menu or something.
            // By not setting the entity as the active object, twe let this
            // "game level" (the console we're hosting the entity on) determine if
            // the keyboard data should be sent to the entity.

            // Process logic for moving the entity.
            bool keyHit = false;

            if (info.IsKeyReleased(Keys.Up))
            {
                _player.Position = new Point(_player.Position.X, _player.Position.Y - 1);
                keyHit = true;
            }
            else if (info.IsKeyReleased(Keys.Down))
            {
                _player.Position = new Point(_player.Position.X, _player.Position.Y + 1);
                keyHit = true;
            }

            if (info.IsKeyReleased(Keys.Left))
            {
                _player.Position = new Point(_player.Position.X - 1, _player.Position.Y);
                keyHit = true;
            }
            else if (info.IsKeyReleased(Keys.Right))
            {
                _player.Position = new Point(_player.Position.X + 1, _player.Position.Y);
                keyHit = true;
            }


            if (keyHit)
            {
                // Entity moved. Let's draw a trail of where they moved from.

                // We are not detecting when the player tries to move off the console area.
                // We could detected that though and then move the player back to where they were.
                _textSurface.SetCharacter(_playerPreviousPosition.X, _playerPreviousPosition.Y, 250);
                _playerPreviousPosition = _player.Position;

                return true;
            }

            // You could have multiple entities in the game for example, and change
            // which entity gets keyboard commands.
            return false;
        }

        /// <summary>
        /// Player entity class
        /// </summary>
        class Player : Entity
        {
            public Player()
            {
                // Update the default animation frame (cellsurface that is 1x1 of nothing) to have a smiley character
                _currentAnimation.CurrentFrame[0].CharacterIndex = 1;
            }

        }
    }
}