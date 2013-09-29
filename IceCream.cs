using System;
using System.Drawing;
using System.Windows.Forms;


namespace IceCream {

	public class IceCream : Form{
		public IceCream(){

			this.Text = "Ice_Cream";
			this.Size = new System.Drawing.Size(220, 550);
			this.Load += new EventHandler(this.Form_Load);

			// Add button callbacks
			this.btnRand.Click += new EventHandler(this.btnRand_clik);

			this.Controls.Add(this.btnRand);
			this.Show();
		}

		private PictureBox pictureBox = new PictureBox();

		private Graphics canv; //Холст


		private IceCreamImage iceCreamImage = new IceCreamImage(
			cone:   Image.FromFile(@"pic/cone.png"),
			scoops: new Image[] {
				Image.FromFile(@"pic/scoop_0.png"),
			    Image.FromFile(@"pic/scoop_1.png"),
			    Image.FromFile(@"pic/scoop_2.png")
			}
		);


		//Кнопки:
		private Button btnRand = new IceCreamButton(
			width:      100,
			height:     50,
			text:       "Moar, please!",
			dock:       DockStyle.Bottom
		);

		// private Button btnCancel; // Will be used in future versions


		private void pictureBox1_Paint (object sender, System.Windows.Forms.PaintEventArgs e){
			//Инициализация холста
			canv = e.Graphics;
			DrawGrid();

			iceCreamImage.drawConeOn(canv);
			iceCreamImage.drawScoopOn(canv);
		}


		private void btnRand_clik (object sender, System.EventArgs e){
			iceCreamImage.changeScoop();

			Console.WriteLine("btnRand cliked!!!");
			pictureBox.Refresh();
		}


		private void Form_Load(object sender, System.EventArgs e){
			// Choose random scoop
			iceCreamImage.changeScoop();
			// Dock the PictureBox to the form and set its background to white.
			this.Padding = new System.Windows.Forms.Padding(10);
			pictureBox.Dock = DockStyle.Fill;
	    	pictureBox.BackColor = Color.Gray;
			// Connect the Paint event of the PictureBox to the event handler method.
	    	pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// Add the PictureBox control to the Form.
	    	this.Controls.Add(pictureBox);
		}


		private void DrawGrid(int step=50){
			for (int x=0; x<this.pictureBox.Height; x+=step) 
				canv.DrawLine (System.Drawing.Pens.Red, 0, x, this.pictureBox.Width, x);
			for (int x=0; x<this.pictureBox.Width; x+=step)
				canv.DrawLine (System.Drawing.Pens.Green, x, 0, x, this.pictureBox.Height);
		}

	}



	internal class IceCreamButton : Button {
		public IceCreamButton(int width, int height, string text, DockStyle dock) {
			/*
				Get Button params, renders and returns Button object
			*/

			this.Size = new Size(width, height);
			this.Text = text;
			this.Dock = dock;
			this.Show();
		}
	}



	internal class IceCreamImage {
		public IceCreamImage(Image cone, Image[] scoops) {
			this.cone = cone;
			this.scoops = scoops;
		}


		public void changeScoop() {
			this.scoop = this.scoops[this.rnd.Next(this.scoops.Length)];
		}


		public void drawConeOn(Graphics canvas) {
			canvas.DrawImage(this.cone, new Rectangle(50, 300, 100, 150));
		}


		public void drawScoopOn(Graphics canvas) {
			Image scoop = this.scoop;
			Size scoopSize = new Size(100, scoop.Width / scoop.Height * 100);
			canvas.DrawImage(scoop, new Rectangle(new Point(50, 200), scoopSize));
		}


		public Image cone;
		public Image scoop;

		protected Image[] scoops;
		protected Random rnd = new Random();
	}

}
