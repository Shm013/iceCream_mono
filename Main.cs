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

		this.makeRandIceCream(); //рандомим мороженку
		this.Padding = new System.Windows.Forms.Padding(10); //отступы
		pictureBox.Dock = DockStyle.Fill; //pictureBox на все свободное место
    	pictureBox.BackColor = Color.Gray; //заливочка
    	pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint); //отрисовка
    	this.Controls.Add(pictureBox);

		//добовление кнопочки
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
	//Кнопки:
	private Button btnRand;
	//Текущие состояние, размры и место для отрисовки:
	private Image[] scoop = new Image[3]; //3-x этажное мороженое
	private Image cone;
	private Point scoopPt = new Point(50,250);
	private Size scoopSz = new Size(100,70);

	//Ивент отрисовки
	private void pictureBox1_Paint (object sender, System.Windows.Forms.PaintEventArgs e){
		//Инициализация холста
		canv = e.Graphics;
		DrawGrid();
		DrawCone();
		DrawScoops();
	}
	//ивент нажатия на кнопку
	private void btnRand_clik(object sender, System.EventArgs e){
		this.makeRandIceCream();
		//Console.WriteLine("btnRand cliked!!!");
		pictureBox.Refresh();
	}

	//сдеть рандомную мороженку
	private void makeRandIceCream(){
		for(int i=(scoop.Length-1); i>=0 ;i--)
			scoop[i]=scoopImg[random.Next(this.scoopImg.Length)];
		//scoop[2]=scoopImg[0];
		cone = coneImg;
	}
	//рисование шариков
	private void DrawScoops(){
		Point scoopPt = this.scoopPt; 
		//Size scoopSz = new Size (100, 100);
		for (int i=(scoop.Length-1); i>=0; i--){
			canv.DrawImage (scoop[i], new Rectangle (scoopPt, scoopSz));
			scoopPt.Y-=40;
		}
	}
	//рисование рожка
	private void DrawCone(){
		canv.DrawImage(cone, new Rectangle(50,300,100,150));
	}
	//рисование сетки for test.
	private void DrawGrid(int step=50){
		for (int x=0; x<this.pictureBox.Height; x+=step) 
			canv.DrawLine (System.Drawing.Pens.Red, 0, x, this.pictureBox.Width, x);
		for (int x=0; x<this.pictureBox.Width; x+=step)
			canv.DrawLine (System.Drawing.Pens.Green, x, 0, x, this.pictureBox.Height);
	}

}