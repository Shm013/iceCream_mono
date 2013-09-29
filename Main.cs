//IceCream by Shm
//Рисование рандомного мороженого в окне

using System;
using System.Drawing;
using System.Windows.Forms;

class Programm{
	public static void Main(){
		Application.Run(new iceCream());
	}
}
class iceCream : Form{
	public iceCream(){

		this.Text="Ice_Cream";
		this.Size= new System.Drawing.Size(220,550);
		this.Load += new EventHandler(this.Form_Load);

		this.btnRand = new Button();
		this.btnRand.Size = new Size(100,50);
		this.btnRand.Text = "Moar, please!";
		this.btnRand.Dock = DockStyle.Bottom;
		this.btnRand.Click += new EventHandler(this.btnRand_clik);

		this.Controls.Add(this.btnRand);
		this.btnRand.Show();
		this.Show();
	}

	private PictureBox pictureBox = new PictureBox();
	private Graphics canv; //Холст
	private Random random = new Random();
	//Все пикчи сдесь:
	private Image coneImg = Image.FromFile(@"pic/cone.png");
	private Image[] scoopImg = {Image.FromFile(@"pic/scoop_0.png"),
		                        Image.FromFile(@"pic/scoop_1.png"),
							    Image.FromFile(@"pic/scoop_2.png")
	};
	//Текущие пикчи:
	private Image scoop;

	private void pictureBox1_Paint (object sender, System.Windows.Forms.PaintEventArgs e){
		//Инициализация холста
		canv = e.Graphics;
		DrawGrid();
		DrawCone();
		DrawScoop();
	}

	//Кнопки:
	private Button btnRand;
	private Button btnCancel;

	private void btnRand_clik (object sender, System.EventArgs e){
		this.scoop = this.scoopImg[this.random.Next(this.scoopImg.Length)];
		Console.WriteLine("btnRand cliked!!!");
		pictureBox.Refresh();
	}

	private void Form_Load(object sender, System.EventArgs e){
		//Init start Image:
		this.scoop=scoopImg[this.random.Next(this.scoopImg.Length)];
		// Dock the PictureBox to the form and set its background to white.
		this.Padding = new System.Windows.Forms.Padding(10);
		pictureBox.Dock = DockStyle.Fill;
    	pictureBox.BackColor = Color.Gray;
		// Connect the Paint event of the PictureBox to the event handler method.
    	pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
		// Add the PictureBox control to the Form.
    	this.Controls.Add(pictureBox);
	}

	private void DrawScoop (){
		Size scoopSz = new Size(100,scoop.Width/scoop.Height*100);
		canv.DrawImage(scoop, new Rectangle(new Point(50,200),scoopSz));
	}

	private void DrawCone(){
		canv.DrawImage(coneImg, new Rectangle(50,300,100,150));
	}

	private void DrawGrid(int step=50){
		for (int x=0; x<this.pictureBox.Height; x+=step) 
			canv.DrawLine (System.Drawing.Pens.Red, 0, x, this.pictureBox.Width, x);
		for (int x=0; x<this.pictureBox.Width; x+=step)
			canv.DrawLine (System.Drawing.Pens.Green, x, 0, x, this.pictureBox.Height);
	}

}