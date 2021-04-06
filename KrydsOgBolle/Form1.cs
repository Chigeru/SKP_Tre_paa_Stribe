using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrydsOgBolle
{
    public partial class Form1 : Form
    {
        int player = 0;

        //Holder styr på spillet her bagved i stedet for på GUI
        List<String> board = new List<string>();
        public Form1()
        {
            InitializeComponent();
            
            Player_turn();
            int i = 0;

            //Får fat på knapperne igennem Form1 og opdaterer dem
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Click += new System.EventHandler(this.Box_Chooser);
                button.Font = new Font(button.Font.FontFamily, 72);
                board.Add(" ");
                button.Text = " ";
                
                //Bruges som en falsk form for ID, for nemmere kunne tilgåes igennem board array
                button.Tag = i;
                button.TabStop = false;
                i++;
            }
        }

        //Opdaterer spillerens valgte felt med spillerens tegn
        private void Box_Chooser(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;
            if (player == 1)
            {
                board[(Int32)triggeredButton.Tag] = "X";
                triggeredButton.Text = board[(Int32)triggeredButton.Tag];
            }
            else
            {
                board[(Int32)triggeredButton.Tag] = "O";
                triggeredButton.Text = board[(Int32)triggeredButton.Tag];
            }
            triggeredButton.Enabled = false;
            Win_Checker();
            Player_turn();
        }

        //Skifter spillerens tur og opdaterer label i toppen
        private void Player_turn()
        {
            if (player != 1)
            {
                player = 1;
                label1.Text = $"Player {player}'s turn - X";
            }
            else
            {
                player = 2;
                label1.Text = $"Player {player}'s turn - 0";
            }
        }

        //Tjekker om der er vundet eller om pladen er fyldt
        private void Win_Checker()
        {
            //Tjekker vandret 
            if (board[0] == board[1] && board[1] == board[2] && board[0] != " ") { Win_Annoucer(true); }
            else if (board[3] == board[4] && board[4] == board[5] && board[3] != " ") { Win_Annoucer(true); }
            else if (board[6] == board[7] && board[7] == board[8] && board[6] != " ") { Win_Annoucer(true); }

            //Tjekker på kryds
            else if (board[0] == board[4] && board[4] == board[8] && board[0] != " ") { Win_Annoucer(true); }
            else if (board[2] == board[4] && board[4] == board[6] && board[2] != " ") { Win_Annoucer(true); }

            //Tjekker lodret
            else if (board[0] == board[3] && board[3] == board[6] && board[0] != " ") { Win_Annoucer(true); }
            else if (board[1] == board[4] && board[4] == board[7] && board[1] != " ") { Win_Annoucer(true); }
            else if (board[2] == board[5] && board[5] == board[8] && board[2] != " ") { Win_Annoucer(true); }

            //Tjekker om alle felter er udfyldte
            else if (board[0] != " " &&
                board[1] != " " &&
                board[2] != " " &&
                board[3] != " " &&
                board[4] != " " &&
                board[5] != " " &&
                board[6] != " " &&
                board[7] != " " &&
                board[8] != " ") { Win_Annoucer(false); }
        }

        private void Win_Annoucer(bool winner)
        {
            //Disabler alle felterne
            foreach (Button b in this.Controls.OfType<Button>())
                b.Enabled = false;

            //Besked til dialog vinduet
            string message = "";
            if (winner)
                message = $"Player {player} has won!!!\ntry again?";
            else
                message = "No more moves available \ntry again?";

            //Dialogboxen som vises efter spillet er slut
            DialogResult res = MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
                Application.Restart();
            else if (res == DialogResult.No)
                Application.Exit();
            else
                Application.Exit();
        }
    }
}
