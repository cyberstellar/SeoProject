using System;
using System.Windows.Forms;

namespace Test
{
    public partial class Parameters : Form
    {
        private readonly Factors _nf = new Factors(); // new_factors
        private readonly Factors _of = MainForm.Factors; // old_factors

        public Parameters()
        {
            InitializeComponent();

            num_a.Value = _nf.A = _of.A;
            num_b.Value = _nf.B = _of.B;
            num_c.Value = _nf.C = _of.C;
            num_d.Value = _nf.D = _of.D;
            num_e.Value = _nf.E = _of.E;
            num_f.Value = _nf.F = _of.F;
            num_g.Value = _nf.G = _of.G;
            num_h.Value = _nf.H = _of.H;
            num_i.Value = _nf.I = _of.I;
            num_j.Value = _nf.J = _of.J;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (_nf.A != num_a.Value)
                {
                    var orem = 100 - _nf.A; // Старый остаток
                    var nrem = 100 - num_a.Value; // Новый остаток
                    _nf.A = num_a.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.B != num_b.Value)
                {
                    var orem = 100 - _nf.B; // Старый остаток
                    var nrem = 100 - num_b.Value; // Новый остаток
                    _nf.B = num_b.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.C != num_c.Value)
                {
                    var orem = 100 - _nf.C; // Старый остаток
                    var nrem = 100 - num_c.Value; // Новый остаток
                    _nf.C = num_c.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.D != num_d.Value)
                {
                    var orem = 100 - _nf.D; // Старый остаток
                    var nrem = 100 - num_d.Value; // Новый остаток
                    _nf.D = num_d.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.E != num_e.Value)
                {
                    var orem = 100 - _nf.E; // Старый остаток
                    var nrem = 100 - num_e.Value; // Новый остаток
                    _nf.E = num_e.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.F != num_f.Value)
                {
                    var orem = 100 - _nf.F; // Старый остаток
                    var nrem = 100 - num_f.Value; // Новый остаток
                    _nf.F = num_f.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.G != num_g.Value)
                {
                    var orem = 100 - _nf.G; // Старый остаток
                    var nrem = 100 - num_g.Value; // Новый остаток
                    _nf.G = num_g.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.H != num_h.Value)
                {
                    var orem = 100 - _nf.H; // Старый остаток
                    var nrem = 100 - num_h.Value; // Новый остаток
                    _nf.H = num_h.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.I != num_i.Value)
                {
                    var orem = 100 - _nf.I; // Старый остаток
                    var nrem = 100 - num_i.Value; // Новый остаток
                    _nf.I = num_i.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.J = num_j.Value = _nf.J * mult;
                }
                else if (_nf.J != num_j.Value)
                {
                    var orem = 100 - _nf.J; // Старый остаток
                    var nrem = 100 - num_j.Value; // Новый остаток
                    _nf.J = num_j.Value;
                    var mult = nrem / orem; // Множитель
                    _nf.A = num_a.Value = _nf.A * mult;
                    _nf.B = num_b.Value = _nf.B * mult;
                    _nf.C = num_c.Value = _nf.C * mult;
                    _nf.D = num_d.Value = _nf.D * mult;
                    _nf.E = num_e.Value = _nf.E * mult;
                    _nf.F = num_f.Value = _nf.F * mult;
                    _nf.G = num_g.Value = _nf.G * mult;
                    _nf.H = num_h.Value = _nf.H * mult;
                    _nf.I = num_i.Value = _nf.I * mult;
                }

                var sum = _nf.A + _nf.B + _nf.C + _nf.D + _nf.E + _nf.F + _nf.G + _nf.H + _nf.I + _nf.J;

                label_score.Text = $@"{Math.Round(sum, 0)}\r\n—\r\n100";
            }
            catch
            {
                MessageBox.Show(@"Упс! Что-то пошло не так.");
                Close();
            }
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            MainForm.Factors = _nf;
            DialogResult = DialogResult.OK;
        }
    }
}