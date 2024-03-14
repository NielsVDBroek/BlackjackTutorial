using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackTutorial
{
    public class Dealer : Player
    {
        public Dealer(string name, int startBalance) : base(name, startBalance)
        {
            string dealerName = name;
            int dealerBalance = startBalance;
        }

        public void dealCard(Player player)
        {
            //Code voor dealen hier.
            
        }
    }
}
