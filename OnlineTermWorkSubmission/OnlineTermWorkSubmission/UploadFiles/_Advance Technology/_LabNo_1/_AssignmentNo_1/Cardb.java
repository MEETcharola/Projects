import java.awt.*;
import java.awt.event.*;
class CB extends Frame implements ActionListener,ItemListener
{
	Button b1,b2,b3,b4;
	Label l,l1;
	Choice c1;
	CardLayout cl;
	Panel p1,p2,p3,p4,p5;
	CB()
	{
		setTitle("CardBoard");
		setSize(500,500);
		setVisible(true);
		p1=new Panel();
		p2=new Panel();
		p3=new Panel();
		p4=new Panel();
		p5=new Panel();
		p3.setLayout(new GridLayout(1,5));
		b1=new Button("First");
		b2=new Button("Previous");
		b3=new Button("Next");
		b4=new Button("Last");
		c1=new Choice();
		l=new Label("ONE");
		l1=new Label("TWO");
		c1.add("1");
		c1.add("2");
		//c1.add("3");
		//c1.add("4");
		p3.add(b1);
		p3.add(b2);
		p3.add(b3);
		p3.add(b4);
		p3.add(c1);
		p4.add(l);
		p5.add(l1);
		p1.setLayout(new GridLayout(2,1));
		cl=new CardLayout();
		p2.setLayout(cl);
		p1.add(p2);
		p1.add(p3);
		add(p1);
		p2.add(p4,"First");
		p2.add(p5,"Second");
		b1.addActionListener(this);
		b2.addActionListener(this);
		b3.addActionListener(this);
		b4.addActionListener(this);
		c1.addItemListener(this);
	}
	public void actionPerformed(ActionEvent a)
	{
		if(a.getSource()==b3)
		{
			cl.next(p2);
		}
		if(a.getSource()==b1)
		{
			cl.first(p2);
		}
		if(a.getSource()==b2)
		{
			cl.previous(p2);
		}
		if(a.getSource()==b4)
		{
			cl.last(p2);
		}
	}
	public void itemStateChanged(ItemEvent i)
	{
			if(c1.getSelectedIndex()==0)
				cl.show(p2,"First");
			else
				cl.show(p2,"Second");
	}
}
class Cardb
{
	public static void main(String args[])
	{
		new CB();
	}
}
		