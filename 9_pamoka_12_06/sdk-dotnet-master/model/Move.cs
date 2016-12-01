namespace CodeFights.model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Move
    {
        private readonly List<Area> attacks = new List<Area>();

        private readonly List<Area> defences = new List<Area>();

        public IList<Area> Attacks
        {
            get
            {
                return this.attacks;
            }
        }

        public IList<Area> Defences
        {
            get
            {
                return this.defences;
            }
        }

        public string Comment { get; set; }

        public Move AddAttack(Area area)
        {
            this.attacks.Add(area);
            return this;
        }

        public Move AddDefence(Area area)
        {
            this.defences.Add(area);
            return this;
        }

        public Move SetComment(string comment)
        {
            this.Comment = comment;
            return this;
        }

        public override string ToString()
        {
            StringBuilder rez = new StringBuilder("Move ");

            foreach (Area attack in attacks)
                rez.Append(" ATTACK " + attack);

            foreach (Area defence in defences)
                rez.Append(" BLOCK " + defence);

            if (!string.IsNullOrWhiteSpace(Comment))
                rez.Append(" COMMENT " + Comment);

            return rez.ToString();
        }
    }
}