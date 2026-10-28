using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_FaxRequest : Report_Template_CoverPage
{
	private IContainer components;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label7;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label_CompanyName;

	private Label label_SerialNumber;

	private Label label_SiteCode;

	private Label label_ToCompany;

	private Label label_To;

	private Label label_ToFax;

	private Label label_ToPhone;

	private Label label_From;

	private Label label_FromFax;

	private Label label_Date;

	private Label label_Pages;

	private Label label_Subject;

	public string From
	{
		set
		{
			label_From.Text = value;
			Refresh();
		}
	}

	public string FromFax
	{
		set
		{
			label_FromFax.Text = value;
		}
	}

	public string To
	{
		set
		{
			label_To.Text = value;
		}
	}

	public string ToCompany
	{
		set
		{
			label_ToCompany.Text = value;
		}
	}

	public string ToFax
	{
		set
		{
			label_ToFax.Text = value;
		}
	}

	public string ToPhone
	{
		set
		{
			label_ToPhone.Text = value;
		}
	}

	public DateTime Date
	{
		set
		{
			label_Date.Text = value.ToShortDateString();
		}
	}

	public string Subject
	{
		set
		{
			label_Subject.Text = value;
		}
	}

	public int Pages
	{
		set
		{
			label_Pages.Text = value.ToString();
		}
	}

	public string LicenseCompanyName
	{
		set
		{
			label_CompanyName.Text = value;
		}
	}

	public string LicenseSerialNumber
	{
		set
		{
			label_SerialNumber.Text = value;
		}
	}

	public string LicenseSiteCode
	{
		set
		{
			label_SiteCode.Text = value;
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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label_CompanyName = new System.Windows.Forms.Label();
		this.label_SerialNumber = new System.Windows.Forms.Label();
		this.label_SiteCode = new System.Windows.Forms.Label();
		this.label_ToCompany = new System.Windows.Forms.Label();
		this.label_To = new System.Windows.Forms.Label();
		this.label_ToFax = new System.Windows.Forms.Label();
		this.label_ToPhone = new System.Windows.Forms.Label();
		this.label_From = new System.Windows.Forms.Label();
		this.label_FromFax = new System.Windows.Forms.Label();
		this.label_Date = new System.Windows.Forms.Label();
		this.label_Pages = new System.Windows.Forms.Label();
		this.label_Subject = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(16, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(49, 25);
		this.label1.TabIndex = 0;
		this.label1.Text = "To: ";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(16, 54);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(115, 25);
		this.label2.TabIndex = 0;
		this.label2.Text = "Company: ";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(16, 124);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(167, 25);
		this.label3.TabIndex = 0;
		this.label3.Text = "Phone Number: ";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Black;
		this.label4.Location = new System.Drawing.Point(16, 89);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(141, 25);
		this.label4.TabIndex = 0;
		this.label4.Text = "Fax Number: ";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.Black;
		this.label5.Location = new System.Drawing.Point(16, 183);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(73, 25);
		this.label5.TabIndex = 0;
		this.label5.Text = "From: ";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.Black;
		this.label7.Location = new System.Drawing.Point(16, 218);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(141, 25);
		this.label7.TabIndex = 0;
		this.label7.Text = "Fax Number: ";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Location = new System.Drawing.Point(16, 284);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 25);
		this.label8.TabIndex = 0;
		this.label8.Text = "Date: ";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.Black;
		this.label9.Location = new System.Drawing.Point(16, 320);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(226, 25);
		this.label9.TabIndex = 0;
		this.label9.Text = "Pages (incl. this one): ";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.Black;
		this.label10.Location = new System.Drawing.Point(16, 355);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(96, 25);
		this.label10.TabIndex = 0;
		this.label10.Text = "Subject: ";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.Black;
		this.label11.Location = new System.Drawing.Point(16, 441);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(112, 25);
		this.label11.TabIndex = 0;
		this.label11.Text = "Message: ";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.Color.Black;
		this.label12.Location = new System.Drawing.Point(17, 480);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(476, 20);
		this.label12.TabIndex = 1;
		this.label12.Text = "Please issue the following installation of SphereIQ a activation code:";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(17, 510);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(0, 20);
		this.label13.TabIndex = 1;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.ForeColor = System.Drawing.Color.Black;
		this.label14.Location = new System.Drawing.Point(16, 542);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(111, 20);
		this.label14.TabIndex = 1;
		this.label14.Text = "Serial number:";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.ForeColor = System.Drawing.Color.Black;
		this.label15.Location = new System.Drawing.Point(17, 511);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(126, 20);
		this.label15.TabIndex = 1;
		this.label15.Text = "Company Name:";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.Black;
		this.label16.Location = new System.Drawing.Point(16, 575);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(83, 20);
		this.label16.TabIndex = 1;
		this.label16.Text = "Site Code:";
		this.label_CompanyName.AutoSize = true;
		this.label_CompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CompanyName.ForeColor = System.Drawing.Color.Black;
		this.label_CompanyName.Location = new System.Drawing.Point(149, 511);
		this.label_CompanyName.Name = "label_CompanyName";
		this.label_CompanyName.Size = new System.Drawing.Size(33, 20);
		this.label_CompanyName.TabIndex = 1;
		this.label_CompanyName.Text = "null";
		this.label_SerialNumber.AutoSize = true;
		this.label_SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SerialNumber.ForeColor = System.Drawing.Color.Black;
		this.label_SerialNumber.Location = new System.Drawing.Point(149, 542);
		this.label_SerialNumber.Name = "label_SerialNumber";
		this.label_SerialNumber.Size = new System.Drawing.Size(33, 20);
		this.label_SerialNumber.TabIndex = 1;
		this.label_SerialNumber.Text = "null";
		this.label_SiteCode.AutoSize = true;
		this.label_SiteCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SiteCode.ForeColor = System.Drawing.Color.Black;
		this.label_SiteCode.Location = new System.Drawing.Point(149, 575);
		this.label_SiteCode.Name = "label_SiteCode";
		this.label_SiteCode.Size = new System.Drawing.Size(33, 20);
		this.label_SiteCode.TabIndex = 1;
		this.label_SiteCode.Text = "null";
		this.label_ToCompany.AutoSize = true;
		this.label_ToCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_ToCompany.ForeColor = System.Drawing.Color.Black;
		this.label_ToCompany.Location = new System.Drawing.Point(240, 53);
		this.label_ToCompany.Name = "label_ToCompany";
		this.label_ToCompany.Size = new System.Drawing.Size(40, 24);
		this.label_ToCompany.TabIndex = 3;
		this.label_ToCompany.Text = "null";
		this.label_To.AutoSize = true;
		this.label_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_To.ForeColor = System.Drawing.Color.Black;
		this.label_To.Location = new System.Drawing.Point(240, 19);
		this.label_To.Name = "label_To";
		this.label_To.Size = new System.Drawing.Size(40, 24);
		this.label_To.TabIndex = 3;
		this.label_To.Text = "null";
		this.label_ToFax.AutoSize = true;
		this.label_ToFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_ToFax.ForeColor = System.Drawing.Color.Black;
		this.label_ToFax.Location = new System.Drawing.Point(240, 89);
		this.label_ToFax.Name = "label_ToFax";
		this.label_ToFax.Size = new System.Drawing.Size(40, 24);
		this.label_ToFax.TabIndex = 3;
		this.label_ToFax.Text = "null";
		this.label_ToPhone.AutoSize = true;
		this.label_ToPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_ToPhone.ForeColor = System.Drawing.Color.Black;
		this.label_ToPhone.Location = new System.Drawing.Point(240, 124);
		this.label_ToPhone.Name = "label_ToPhone";
		this.label_ToPhone.Size = new System.Drawing.Size(40, 24);
		this.label_ToPhone.TabIndex = 3;
		this.label_ToPhone.Text = "null";
		this.label_From.AutoSize = true;
		this.label_From.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_From.ForeColor = System.Drawing.Color.Black;
		this.label_From.Location = new System.Drawing.Point(240, 183);
		this.label_From.Name = "label_From";
		this.label_From.Size = new System.Drawing.Size(40, 24);
		this.label_From.TabIndex = 3;
		this.label_From.Text = "null";
		this.label_FromFax.AutoSize = true;
		this.label_FromFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_FromFax.ForeColor = System.Drawing.Color.Black;
		this.label_FromFax.Location = new System.Drawing.Point(240, 218);
		this.label_FromFax.Name = "label_FromFax";
		this.label_FromFax.Size = new System.Drawing.Size(40, 24);
		this.label_FromFax.TabIndex = 3;
		this.label_FromFax.Text = "null";
		this.label_Date.AutoSize = true;
		this.label_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Date.ForeColor = System.Drawing.Color.Black;
		this.label_Date.Location = new System.Drawing.Point(240, 284);
		this.label_Date.Name = "label_Date";
		this.label_Date.Size = new System.Drawing.Size(40, 24);
		this.label_Date.TabIndex = 3;
		this.label_Date.Text = "null";
		this.label_Pages.AutoSize = true;
		this.label_Pages.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Pages.ForeColor = System.Drawing.Color.Black;
		this.label_Pages.Location = new System.Drawing.Point(240, 321);
		this.label_Pages.Name = "label_Pages";
		this.label_Pages.Size = new System.Drawing.Size(40, 24);
		this.label_Pages.TabIndex = 3;
		this.label_Pages.Text = "null";
		this.label_Subject.AutoSize = true;
		this.label_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Subject.ForeColor = System.Drawing.Color.Black;
		this.label_Subject.Location = new System.Drawing.Point(240, 356);
		this.label_Subject.Name = "label_Subject";
		this.label_Subject.Size = new System.Drawing.Size(40, 24);
		this.label_Subject.TabIndex = 3;
		this.label_Subject.Text = "null";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label_To);
		base.Controls.Add(this.label_Subject);
		base.Controls.Add(this.label_Pages);
		base.Controls.Add(this.label_Date);
		base.Controls.Add(this.label_FromFax);
		base.Controls.Add(this.label_From);
		base.Controls.Add(this.label_ToPhone);
		base.Controls.Add(this.label_ToFax);
		base.Controls.Add(this.label_ToCompany);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label_SiteCode);
		base.Controls.Add(this.label_SerialNumber);
		base.Controls.Add(this.label_CompanyName);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label1);
		base.Name = "Report_FaxRequest";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Report_FaxRequest()
	{
		InitializeComponent();
	}
}
