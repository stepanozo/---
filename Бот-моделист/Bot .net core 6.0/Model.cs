using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_.net_core_6._0
{
    internal class Model
    {
        public enum Nations 
        {USSR, Germany, USA, France, Britain, Italy, Sweden, Poland, China, Japan, Czechoslovakia }

        public enum Difficulties
        { light, medium, difficult, difficultPlus}

        public enum Class
        { light, medium, heavy, PT, SAU, diorama}

        public string name;
        public Nations nation;
        public Difficulties difficulty;
        public Class classOfModel;
        public string FileLink;
        public string PictureLink;

        public Model(string name, Nations nation, Difficulties difficulty, Class classOfModel, string FileLink, string PictureLink)
        {
            this.name = name;
            this.nation = nation;
            this.difficulty = difficulty;
            this.classOfModel = classOfModel;
            this.FileLink = FileLink;
            this.PictureLink = PictureLink;
        }
       
    }
}
