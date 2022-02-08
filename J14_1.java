import java.awt.BorderLayout;
import java.awt.Canvas;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics;
import java.awt.Image;

import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JSlider;
import javax.swing.RepaintManager;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

public class J14_1 extends JFrame implements ChangeListener{
	NewCanvas myCanvas;
	JSlider A;
	JSlider B;
	JLabel a;
	JLabel b;
	int t = 50;
	int r = 50;

	J14_1(String title) {
		super(title);
		RepaintManager currentManager = RepaintManager.currentManager(this);
		currentManager.setDoubleBufferingEnabled(false);
		setLayout(new FlowLayout());
		A = new JSlider(1, 100, t);
		B = new JSlider(1, 100, r);
		a = new JLabel(Integer.toString(t));
		b = new JLabel(Integer.toString(r));
		A.addChangeListener(this);
		B.addChangeListener(this);
		myCanvas = new NewCanvas();
		myCanvas.setSize(300, 300);
		add(myCanvas);
		add(A, BorderLayout.CENTER);
		add(a, BorderLayout.CENTER);
		add(B, BorderLayout.CENTER);
		add(b, BorderLayout.CENTER);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setSize(340, 460);
		setVisible(true);
		myCanvas.init();
		while(true) {
			myCanvas.setT(t);
			myCanvas.setR(r);
			myCanvas.repaint();
			try {
				Thread.sleep(1);	
			}
			catch(InterruptedException e) {
			}
		}
	}
	
	public void stateChanged(ChangeEvent e) {
		if(e.getSource() == A) {
			t = A.getValue();
			a.setText(Integer.toString(t));
		}
		else {
			r = B.getValue();
			b.setText(Integer.toString(r));
		}
	}
	
	public static void main(String[] args) {
		new J14_1("課題1");
	}

}

class NewCanvas extends Canvas{
	Dimension size;
	Image back;
	Graphics buffer;
	int T = 50;
	int R = 50;
	int time = 0;
	int color;
	
	public void init() {
		size = getSize();
		back = createImage(size.width, size.height);
		buffer = back.getGraphics();
	}
	
	public void update(Graphics g) {
		paint(g);
	}
	
	void setT(int t) {
		T = t;
	}
	
	void setR(int r) {
		R = r;
	}
	
	public void paint(Graphics g) {
		time += T;
		if(time >= 100000) time -= 100000;
		double S = Math.PI * (double)time / (double)50000;
		int x = (int)(((double)size.width / 2) + (double)R * Math.sin(S));
		int y = (int)(((double)size.height / 2) + (double)R * Math.cos(S));
		int color = time - 50000;
		if(color < 0) color *= -1;
		buffer.setColor(new Color(51 * color / 10000, 51 * color / 10000, 51 * color / 10000));
		for(int i = 0; i < size.width; i++) buffer.drawLine(i, 0, i, size.height - 1);
		buffer.setColor(Color.RED);
		buffer.drawArc(size.width / 2 - R, size.height / 2 - R, 2 * R, 2 * R, 0, 360);
		buffer.setColor(Color.BLUE);
		buffer.fillOval(x-10, y-10, 20, 20);
		g.drawImage(back, 0, 0, this);
	}
}