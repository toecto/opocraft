using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Data;
using System.Diagnostics;

namespace OppoCraft
{
    public class GraphContentManager
    {
        Game1 theGame;
        public Dictionary<string, AnimationFile> files;

        public GraphContentManager(Game1 game)
        {
            this.files = new Dictionary<string, AnimationFile>();
            this.theGame = game;
        }

        public void LoadContent()
        {
            DataTable filesData = this.theGame.db.Query("SELECT * FROM AnimationFile");
            foreach (DataRow AnimationFileData in filesData.Rows)
            {
                Debug.WriteLine("AnimationFileData" + (string)AnimationFileData["Path"]);
                Texture2D texture;
                AnimationFile file;
                string name;
                if ((bool)AnimationFileData["Coloured"])
                {
                    this.files.Add((string)AnimationFileData["Path"], null);//mark that the animation exists and coloured
                    name = "Blue" + (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);

                    name = "Red" + (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);
                }
                else
                {
                    name =  (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);

                }
            }
        }

        public Texture2D LoadTexture(string name)
        {
            return this.theGame.Content.Load<Texture2D>("Animations\\" + name);
        }

        public UnitAnimation GetUnitAnimation(Unit unit, string name)
        {
            AnimationFile file = this.files[name];

            if (file == null)
            {
                if (unit.cid == this.theGame.cid)
                    name = "Blue" + name;
                else
                    name = "Red" + name;
                file = this.files[name];
            }
            

            DataTable actions = this.theGame.db.Query("SELECT * FROM Animation where AnimationFileID=" + file.id);
            UnitAnimation unitAnimation = new UnitAnimation(unit);
            foreach (DataRow action in actions.Rows)
            {
                unitAnimation.Add((string)action["AnimationNameID"], GetActionAnimation(file, action));
            }

            return unitAnimation;
        }

        public ActionAnimation GetActionAnimation(string fileName, string ActionName)
        {
            AnimationFile file = this.files[fileName];
            DataTable actions = this.theGame.db.Query("SELECT * FROM Animation where AnimationFileID=" + file.id + " and AnimationNameID='"+ActionName+"'");
            DataRow action=actions.Rows[0];
            return GetActionAnimation(file,action);
        }

        public ActionAnimation GetActionAnimation(AnimationFile file, DataRow actionData)
        {
            List<SimpleAnimation> actionAnimations = null;
            if ((string)actionData["AnimationMap"] == "Directions")
            {
                actionAnimations = file.getAnimations((int)actionData["StartX"], (int)actionData["StartY"], (int)actionData["Frames"], (int)actionData["Delay"], (bool)actionData["Looped"], 2, 4);
                return new ActionAnimationByDirection((string)actionData["AnimationNameID"], actionAnimations, (int)actionData["Priority"]);
            }
            else
            {
                actionAnimations = file.getAnimations((int)actionData["StartX"], (int)actionData["StartY"], (int)actionData["Frames"], (int)actionData["Delay"], (bool)actionData["Looped"]);
                return new ActionAnimation((string)actionData["AnimationNameID"], actionAnimations, (int)actionData["Priority"]);
            }
        }


    }
}
