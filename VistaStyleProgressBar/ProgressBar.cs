using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VistaStyleProgressBar;

[DefaultEvent("ValueChanged")]
public class ProgressBar : UserControl
{
	public delegate void ValueChangedHandler(object sender, EventArgs e);

	public delegate void MinChangedHandler(object sender, EventArgs e);

	public delegate void MaxChangedHandler(object sender, EventArgs e);

	private Container components;

	private int mGlowPosition = -325;

	private Timer mGlowAnimation = new Timer();

	private int mValue;

	private int mMaxValue = 100;

	private int mMinValue;

	private Color mStartColor = Color.FromArgb(210, 0, 0);

	private Color mEndColor = Color.FromArgb(0, 211, 40);

	private Color mHighlightColor = Color.White;

	private Color mBackgroundColor = Color.FromArgb(201, 201, 201);

	private bool mAnimate = true;

	private Color mGlowColor = Color.FromArgb(150, 255, 255, 255);

	[DefaultValue(0)]
	[Description("The value that is displayed on the progress bar.")]
	[Category("Value")]
	public int Value
	{
		get
		{
			return mValue;
		}
		set
		{
			if (value <= MaxValue && value >= MinValue)
			{
				mValue = value;
				if (value < MaxValue)
				{
					mGlowAnimation.Start();
				}
				if (value == MaxValue)
				{
					mGlowAnimation.Stop();
				}
				ValueChanged?.Invoke(this, new EventArgs());
				Invalidate();
			}
		}
	}

	[DefaultValue(100)]
	[Description("The maximum value for the Value property.")]
	[Category("Value")]
	public int MaxValue
	{
		get
		{
			return mMaxValue;
		}
		set
		{
			mMaxValue = value;
			if (value > MaxValue)
			{
				Value = MaxValue;
			}
			if (Value < MaxValue)
			{
				mGlowAnimation.Start();
			}
			MaxChanged?.Invoke(this, new EventArgs());
			Invalidate();
		}
	}

	[DefaultValue(0)]
	[Description("The minimum value for the Value property.")]
	[Category("Value")]
	public int MinValue
	{
		get
		{
			return mMinValue;
		}
		set
		{
			mMinValue = value;
			if (value < MinValue)
			{
				Value = MinValue;
			}
			MinChanged?.Invoke(this, new EventArgs());
			Invalidate();
		}
	}

	[Category("Bar")]
	[DefaultValue(typeof(Color), "210, 0, 0")]
	[Description("The start color for the progress bar.210, 000, 000 = Red\n210, 202, 000 = Yellow\n000, 163, 211 = Blue\n000, 211, 040 = Green\n")]
	public Color StartColor
	{
		get
		{
			return mStartColor;
		}
		set
		{
			mStartColor = value;
			Invalidate();
		}
	}

	[Description("The end color for the progress bar.210, 000, 000 = Red\n210, 202, 000 = Yellow\n000, 163, 211 = Blue\n000, 211, 040 = Green\n")]
	[Category("Bar")]
	[DefaultValue(typeof(Color), "0, 211, 40")]
	public Color EndColor
	{
		get
		{
			return mEndColor;
		}
		set
		{
			mEndColor = value;
			Invalidate();
		}
	}

	[Description("The color of the highlights.")]
	[DefaultValue(typeof(Color), "White")]
	[Category("Highlights and Glows")]
	public Color HighlightColor
	{
		get
		{
			return mHighlightColor;
		}
		set
		{
			mHighlightColor = value;
			Invalidate();
		}
	}

	[Category("Highlights and Glows")]
	[Description("The color of the background.")]
	[DefaultValue(typeof(Color), "201,201,201")]
	public Color BackgroundColor
	{
		get
		{
			return mBackgroundColor;
		}
		set
		{
			mBackgroundColor = value;
			Invalidate();
		}
	}

	[Description("Whether the glow is animated or not.")]
	[DefaultValue(typeof(bool), "true")]
	[Category("Highlights and Glows")]
	public bool Animate
	{
		get
		{
			return mAnimate;
		}
		set
		{
			mAnimate = value;
			if (value)
			{
				mGlowAnimation.Start();
			}
			else
			{
				mGlowAnimation.Stop();
			}
			Invalidate();
		}
	}

	[Category("Highlights and Glows")]
	[Description("The color of the glow.")]
	[DefaultValue(typeof(Color), "150, 255, 255, 255")]
	public Color GlowColor
	{
		get
		{
			return mGlowColor;
		}
		set
		{
			mGlowColor = value;
			Invalidate();
		}
	}

	public event ValueChangedHandler ValueChanged;

	public event MinChangedHandler MinChanged;

	public event MaxChangedHandler MaxChanged;

	public ProgressBar()
	{
		InitializeComponent();
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.DoubleBuffer, value: true);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
		SetStyle(ControlStyles.Selectable, value: true);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.UserPaint, value: true);
		BackColor = Color.Transparent;
		if (!InDesignMode())
		{
			mGlowAnimation.Tick += mGlowAnimation_Tick;
			mGlowAnimation.Interval = 15;
			if (Value < MaxValue)
			{
				mGlowAnimation.Start();
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.Name = "ProgressBar";
		base.Size = new System.Drawing.Size(264, 32);
		base.Paint += new System.Windows.Forms.PaintEventHandler(this.ProgressBar_Paint);
	}

	private void DrawBackground(Graphics g)
	{
		Rectangle clientRectangle = ClientRectangle;
		clientRectangle.Width--;
		clientRectangle.Height--;
		GraphicsPath path = RoundRect(clientRectangle, 2f, 2f, 2f, 2f);
		g.FillPath(new SolidBrush(BackgroundColor), path);
	}

	private void DrawBackgroundShadows(Graphics g)
	{
		Rectangle rect = new Rectangle(2, 2, 10, Height - 5);
		LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(30, 0, 0, 0), Color.Transparent, LinearGradientMode.Horizontal);
		rect.X--;
		g.FillRectangle(brush, rect);
		Rectangle rect2 = new Rectangle(Width - 12, 2, 10, Height - 5);
		LinearGradientBrush brush2 = new LinearGradientBrush(rect2, Color.Transparent, Color.FromArgb(20, 0, 0, 0), LinearGradientMode.Horizontal);
		g.FillRectangle(brush2, rect2);
	}

	private void DrawBar(Graphics g)
	{
		Rectangle rect = new Rectangle(1, 2, Width - 3, Height - 3);
		rect.Width = (int)((float)Value * 1f / (float)(MaxValue - MinValue) * (float)Width);
		g.FillRectangle(new SolidBrush(GetIntermediateColor()), rect);
	}

	private void DrawBarShadows(Graphics g)
	{
		Rectangle rect = new Rectangle(1, 2, 15, Height - 3);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.White, Color.White, LinearGradientMode.Horizontal);
		ColorBlend colorBlend = new ColorBlend(3);
		colorBlend.Colors = new Color[3]
		{
			Color.Transparent,
			Color.FromArgb(40, 0, 0, 0),
			Color.Transparent
		};
		colorBlend.Positions = new float[3] { 0f, 0.2f, 1f };
		linearGradientBrush.InterpolationColors = colorBlend;
		rect.X--;
		g.FillRectangle(linearGradientBrush, rect);
		Rectangle rect2 = new Rectangle(Width - 3, 2, 15, Height - 3);
		rect2.X = (int)((float)Value * 1f / (float)(MaxValue - MinValue) * (float)Width) - 14;
		LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect2, Color.Black, Color.Black, LinearGradientMode.Horizontal);
		ColorBlend colorBlend2 = new ColorBlend(3);
		colorBlend2.Colors = new Color[3]
		{
			Color.Transparent,
			Color.FromArgb(40, 0, 0, 0),
			Color.Transparent
		};
		colorBlend2.Positions = new float[3] { 0f, 0.8f, 1f };
		linearGradientBrush2.InterpolationColors = colorBlend2;
		g.FillRectangle(linearGradientBrush2, rect2);
	}

	private void DrawHighlight(Graphics g)
	{
		Rectangle rectangle = new Rectangle(1, 1, Width - 1, 6);
		GraphicsPath graphicsPath = RoundRect(rectangle, 2f, 2f, 0f, 0f);
		g.SetClip(graphicsPath);
		LinearGradientBrush brush = new LinearGradientBrush(rectangle, Color.White, Color.FromArgb(128, Color.White), LinearGradientMode.Vertical);
		g.FillPath(brush, graphicsPath);
		g.ResetClip();
		Rectangle rectangle2 = new Rectangle(1, Height - 8, Width - 1, 6);
		GraphicsPath graphicsPath2 = RoundRect(rectangle2, 0f, 0f, 2f, 2f);
		g.SetClip(graphicsPath2);
		LinearGradientBrush brush2 = new LinearGradientBrush(rectangle2, Color.Transparent, Color.FromArgb(100, HighlightColor), LinearGradientMode.Vertical);
		g.FillPath(brush2, graphicsPath2);
		g.ResetClip();
	}

	private void DrawInnerStroke(Graphics g)
	{
		Rectangle clientRectangle = ClientRectangle;
		clientRectangle.X++;
		clientRectangle.Y++;
		clientRectangle.Width -= 3;
		clientRectangle.Height -= 3;
		GraphicsPath path = RoundRect(clientRectangle, 2f, 2f, 2f, 2f);
		g.DrawPath(new Pen(Color.FromArgb(100, Color.White)), path);
	}

	private void DrawGlow(Graphics g)
	{
		Rectangle rect = new Rectangle(mGlowPosition, 0, 60, Height);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.White, Color.White, LinearGradientMode.Horizontal);
		ColorBlend colorBlend = new ColorBlend(4);
		colorBlend.Colors = new Color[4]
		{
			Color.Transparent,
			GlowColor,
			GlowColor,
			Color.Transparent
		};
		colorBlend.Positions = new float[4] { 0f, 0.5f, 0.6f, 1f };
		linearGradientBrush.InterpolationColors = colorBlend;
		Rectangle clip = new Rectangle(1, 2, Width - 3, Height - 3);
		clip.Width = (int)((float)Value * 1f / (float)(MaxValue - MinValue) * (float)Width);
		g.SetClip(clip);
		g.FillRectangle(linearGradientBrush, rect);
		g.ResetClip();
	}

	private void DrawOuterStroke(Graphics g)
	{
		Rectangle clientRectangle = ClientRectangle;
		clientRectangle.Width--;
		clientRectangle.Height--;
		GraphicsPath path = RoundRect(clientRectangle, 2f, 2f, 2f, 2f);
		g.DrawPath(new Pen(Color.FromArgb(178, 178, 178)), path);
	}

	private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
	{
		float num = r.X;
		float num2 = r.Y;
		float num3 = r.Width;
		float num4 = r.Height;
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddBezier(num, num2 + r1, num, num2, num + r1, num2, num + r1, num2);
		graphicsPath.AddLine(num + r1, num2, num + num3 - r2, num2);
		graphicsPath.AddBezier(num + num3 - r2, num2, num + num3, num2, num + num3, num2 + r2, num + num3, num2 + r2);
		graphicsPath.AddLine(num + num3, num2 + r2, num + num3, num2 + num4 - r3);
		graphicsPath.AddBezier(num + num3, num2 + num4 - r3, num + num3, num2 + num4, num + num3 - r3, num2 + num4, num + num3 - r3, num2 + num4);
		graphicsPath.AddLine(num + num3 - r3, num2 + num4, num + r4, num2 + num4);
		graphicsPath.AddBezier(num + r4, num2 + num4, num, num2 + num4, num, num2 + num4 - r4, num, num2 + num4 - r4);
		graphicsPath.AddLine(num, num2 + num4 - r4, num, num2 + r1);
		return graphicsPath;
	}

	private bool InDesignMode()
	{
		return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
	}

	private Color GetIntermediateColor()
	{
		Color startColor = StartColor;
		Color endColor = EndColor;
		float num = (float)Value * 1f / (float)(MaxValue - MinValue);
		int a = startColor.A;
		int r = startColor.R;
		int g = startColor.G;
		int b = startColor.B;
		int a2 = endColor.A;
		int r2 = endColor.R;
		int g2 = endColor.G;
		int b2 = endColor.B;
		int num2 = (int)Math.Abs((float)a + (float)(a - a2) * num);
		int num3 = (int)Math.Abs((float)r - (float)(r - r2) * num);
		int num4 = (int)Math.Abs((float)g - (float)(g - g2) * num);
		int num5 = (int)Math.Abs((float)b - (float)(b - b2) * num);
		if (num2 > 255)
		{
			num2 = 255;
		}
		if (num3 > 255)
		{
			num3 = 255;
		}
		if (num4 > 255)
		{
			num4 = 255;
		}
		if (num5 > 255)
		{
			num5 = 255;
		}
		return Color.FromArgb(num2, num3, num4, num5);
	}

	private void ProgressBar_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		DrawBackground(e.Graphics);
		DrawBackgroundShadows(e.Graphics);
		DrawBar(e.Graphics);
		DrawBarShadows(e.Graphics);
		DrawHighlight(e.Graphics);
		DrawInnerStroke(e.Graphics);
		DrawGlow(e.Graphics);
		DrawOuterStroke(e.Graphics);
	}

	private void mGlowAnimation_Tick(object sender, EventArgs e)
	{
		if (Animate)
		{
			mGlowPosition += 4;
			if (mGlowPosition > Width)
			{
				mGlowPosition = -300;
			}
			Invalidate();
		}
		else
		{
			mGlowAnimation.Stop();
			mGlowPosition = -320;
		}
	}
}
