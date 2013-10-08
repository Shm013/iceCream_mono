using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace IceCreamApp{ //главное окошечко
	public class iceCreamApp : Form{
		public iceCreamApp(){
			this.Text="Ice_Cream";
			this.Size= new System.Drawing.Size(220,550);

			this.Padding = new System.Windows.Forms.Padding(10); //отступы
			pictureBox.Dock = DockStyle.Fill; //pictureBox на все свободное место
    		pictureBox.BackColor = Color.Gray; //заливочка

			this.grid = new Grid(this.Size);
    		pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);

    		this.Controls.Add(pictureBox);

			this.btnRand.Click += new EventHandler(this.btnRand_clik);
			this.Controls.Add(this.btnRand);
			this.Show();
		}
		private PictureBox pictureBox = new PictureBox();
		private Graphics canv; //Холст
		private Grid grid;     //сетка
		private IceCream iceCream = new IceCream(5);//мороженка
		private Button btnRand = new IceCreamApp.RandomButton(//Кнопка
		    width:      100,
			height:     50,
			text:       "Moar, please!",
			dock:       DockStyle.Bottom
		);

		//ивент нажатия на кнопку
		private void btnRand_clik(object sender, System.EventArgs e){
			this.iceCream.Randomize();
			pictureBox.Refresh();
		}
		//отрисовка тут:
		private void Draw (object sender, System.Windows.Forms.PaintEventArgs e){
			this.canv = e.Graphics;
			grid.DrawOn(canv);
			iceCream.DrawOn(canv);
		}
}

	internal class Grid{ //сетка
		public Grid (Size size, int step=50){
			this.step = step;
			this.size = size;
		}
		public void DrawOn (Graphics canv){
			for (int x=0; x<this.size.Height; x+=step) 
				canv.DrawLine (System.Drawing.Pens.Red, 0, x, this.size.Width, x);
			for (int x=0; x<this.size.Width; x+=step)
				canv.DrawLine (System.Drawing.Pens.Green, x, 0, x, this.size.Height);
		}
		private Size size;
		private int step;
	}

	internal class IceCream{ //собственно мороженка
		public IceCream (int scoopNum=3) //scoopNum - количество шариков
		{
			this.elems.Add (new Element (//добаление рожка в начало
				this.images.getCone (),
				new Size (100, 150),
				new Point (50,300)
			));
			//добавление шариков
			for (int i=scoopNum; i>0; i--) {
				curentPt.Y-=40;
					this.elems.Add (new Element (
					this.images.getScoop (),
					new Size (100, 65),
					this.curentPt
				));
			}
		}
		public void Randomize(){ //зарандомить мороженку
			for(int i=1;i<this.elems.Count;i++)
				this.elems[i].image = this.images.getScoop();
		}
		public void DrawOn (Graphics canv){
			for(int i=0;i<this.elems.Count;i++)
				canv.DrawImage(this.elems[i].image, new Rectangle(this.elems[i].point,this.elems[i].size)); //ForEach не осилел
			}
		//картинки тут
		private IceCreamImage images = new IceCreamImage(
			cone:   Image.FromFile(@"pic/cone.png"),
			scoops: new Image[] {
				Image.FromFile(@"pic/scoop_0.png"),
			    Image.FromFile(@"pic/scoop_1.png"),
			    Image.FromFile(@"pic/scoop_2.png")
			}
		);
		private List<Element> elems = new List<Element>(); //шарики
		private Point curentPt = new Point(50,300);        //рожок
	}

	internal class Element{ //Element - может быть рожком, шариком, и вообще чем угодно...
		public Element(Image img, Size size, Point point){
			this.size = size;
			this.point = point;
			this.image = img;
		}
		public Point point;
		public Image image;
		public Size size;
	}

	internal class RandomButton : Button { //отмены батон
		public RandomButton(int width, int height, string text, DockStyle dock) {
			this.Size = new Size(width, height);
			this.Text = text;
			this.Dock = dock;
			this.Show();
		}
	};

	internal class IceCreamImage{ //для хранения и полчуиния пикчи класс.
		public IceCreamImage(Image cone, Image[] scoops){
			this.cone = cone;
			this.scoops = scoops;
		}
		public Image getCone (){
			return this.cone;
		}
		public Image getScoop(){
			return this.scoops[this.random.Next(this.scoops.Length)];
		}
		private Image cone;
		private Image[] scoops;
		private Random random = new Random();
	}
}