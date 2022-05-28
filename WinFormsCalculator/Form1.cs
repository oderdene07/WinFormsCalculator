using Calculator;
namespace WinFormsCalculator
{
    public partial class CalculatorBase : Form
    {
        Calculator.CalculatorBase dll = new Calculator.CalculatorBase();
        Calculator.Memory memory = new Calculator.Memory();

        long firstNum = 0;
        long secondNum = 0;
        string operation = "";
        bool operationFlag = false;

        public CalculatorBase()
        {
            InitializeComponent();
            MemoryList.RowHeadersVisible = false;
            dll.Result = 0;
        }

        private void NumberClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!operationFlag)
            {
                if (Result.Text == "0")
                {
                    dll.Result = long.Parse(btn.Text);
                }
                else
                {
                    dll.Result = dll.Result * 10 + long.Parse(btn.Text);
                }
                Result.Text = dll.Result.ToString();
            }
            else
            {
                Result.Text = "";
                dll.Result = dll.Result * 10 + long.Parse(btn.Text);
                Result.Text = dll.Result.ToString();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Result.Text = "0";
            firstNum = 0;
            secondNum = 0;
            dll.Result = 0;
            operation = "";
            operationFlag = false;
        }

        private void operationClick(object sender, EventArgs e)
        {
            operationFlag = true;

            Button operationBtn = (Button)sender;
            switch (operationBtn.Text)
            {
                case "+":
                    operation = "+";
                    break;
                case "-":
                    operation = "-";
                    break;
            }
            firstNum = dll.Result;
            dll.Result = 0;
        }
        public long solve()
        {
            switch (operation)
            {
                case "+":
                    return dll.Result + secondNum;
                case "-":
                    return dll.Result - secondNum;
                default: return -1;
            }
        }
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (!operationFlag)
            {
                firstNum = dll.Result;

                if (solve() != -1)
                {
                    dll.Result = solve();
                }
                else
                {
                    dll.Result = firstNum;
                }

                Result.Text = dll.Result.ToString();
            }
            else
            {
                secondNum = dll.Result;
                switch (operation)
                {
                    case "+":
                        dll.Result = firstNum;
                        dll.Result = dll.add(secondNum);
                        Result.Text = dll.Result.ToString();
                        break;
                    case "-":
                        dll.Result = firstNum;
                        dll.Result = dll.subtract(secondNum);
                        Result.Text = dll.Result.ToString();
                        break;
                }
                operationFlag = false;
            }
        }

        private void buttonMemory_Click(object sender, EventArgs e)
        {
            int n = MemoryList.Rows.Add();
            MemoryList.Rows[n].Cells[0].Value = "MC";
            MemoryList.Rows[n].Cells[1].Value = "M+";
            MemoryList.Rows[n].Cells[2].Value = "M-";
            MemoryList.Rows[n].Cells[3].Value = dll.Result.ToString();
            memory.list.Add(new MemoryItem(dll.Result));
            Result.Text = "0";
        }

        private void MemoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            switch (MemoryList.Columns[e.ColumnIndex].Name)
            {
                case "MC":
                    MemoryList.Rows.RemoveAt(rowIndex);
                    memory.list.RemoveAt(rowIndex);
                    break;
                case "mplus":
                    memory.list[rowIndex].MemoryItemAdd(dll.Result);
                    MemoryList.Rows[rowIndex].Cells[3].Value = memory.list[rowIndex].MemoryItemResult;
                    break;
                case "msubtract":
                    memory.list[rowIndex].MemoryItemSubtract(dll.Result);
                    MemoryList.Rows[rowIndex].Cells[3].Value = memory.list[rowIndex].MemoryItemResult;
                    break;
            }
        }
    }
}