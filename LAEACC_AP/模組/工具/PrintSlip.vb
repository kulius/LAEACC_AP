
Imports JBC.Printing

Public Module PrintSlip
    '此函式產生一個空白的收入傳票報表
    Public Function GetIncomeSlipDoc() As FPDocument

        Dim doc As New FPDocument("收入傳票")
        doc.DefaultFont = New Font("新細明體", 12)   '新細明體  標楷體
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.B5
        doc.DefaultPageSettings.Landscape = True
        Dim LeftMargin As Integer = 10
        doc.SetDefaultPageMargin(LeftMargin, 25, 0, 0)   '邊界 LEFT TOP RIGHT BOTTON
        Dim page As New FPPage
        Dim defaultColor As Color = Color.Red
        Dim timesOfBaseWidth As Integer = 4

        '機關名稱
        Dim org As New FPText("測試農田水利會")
        org.Name = "機關名稱"
        org.Font.Name = "標楷體"
        org.Font.Size = 20
        org.SetTextColor(defaultColor)
        org.X = 78
        'org.HAlignment = FPAlignment.Center
        Dim orgSize As SizeF = org.GetSize(GraphicsUnit.Millimeter)

        '機關名稱下方的直線
        Dim orgLine As New FPLine
        orgLine.SetLineWidth(timesOfBaseWidth)
        orgLine.SetLineColor(defaultColor)
        orgLine.X1 = 78
        orgLine.Y1 = org.Y + orgSize.Height + 0.5
        orgLine.X2 = orgLine.X1 + orgSize.Width + 23
        orgLine.Y2 = orgLine.Y1

        '傳票種類
        Dim slip As New FPText("收　入　傳　票")
        slip.Y = org.Y + orgSize.Height + 3
        slip.Font.Name = "標楷體"
        slip.Font.Size = 20
        slip.SetTextColor(defaultColor)
        slip.X = 89
        'slip.HAlignment = FPAlignment.Center
        Dim slipSize As SizeF = slip.GetSize(GraphicsUnit.Millimeter)

        Dim copyKind As New FPText("", New RectangleF(104.5, slip.Y + 8, 22, 8))
        copyKind.Name = "正副本"
        copyKind.Font.Name = "標楷體"
        copyKind.Font.Size = 16
        copyKind.HAlignment = FPAlignment.Center
        copyKind.VAlignment = FPAlignment.Center

        '傳票種類下方的直線
        Dim slipLine1 As New FPLine
        slipLine1.SetLineColor(defaultColor)
        slipLine1.X1 = 89
        slipLine1.Y1 = slip.Y + slipSize.Height + 0
        slipLine1.X2 = slipLine1.X1 + slipSize.Width
        slipLine1.Y2 = slipLine1.Y1
        Dim slipLine2 As New FPLine
        slipLine2.SetLineColor(defaultColor)
        slipLine2.X1 = slipLine1.X1
        slipLine2.Y1 = slipLine1.Y1 + 1
        slipLine2.X2 = slipLine1.X2
        slipLine2.Y2 = slipLine2.Y1

        '製票日期table
        Dim table0 As New FPTable(0, org.Y + orgSize.Height * 2 / 3, 70, 20, 2, 1)
        table0.SetLineColor(defaultColor)
        table0.SetTextColor(defaultColor)
        table0.Font.Name = "新細明體"
        table0.Font.Size = 10
        table0.OutlineThicken(timesOfBaseWidth)
        table0.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(1, 1).PaddingLeft = 2
        table0.Texts2D(1, 1).Text = "製票日期：　　　年　　　月　　　日"
        table0.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(2, 1).PaddingLeft = table0.Cells2D(1, 1).PaddingLeft
        table0.Texts2D(2, 1).Text = "製票編號：收 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t0Year As New FPRectangle(21, 2, 10, 5)
        'Dim t0Month As New FPRectangle(36, 2, 10, 5)
        'Dim t0Day As New FPRectangle(50, 2, 10, 5)
        'Dim t0No As New FPRectangle(34, 12, 26, 5)
        Dim t0Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t0Year.Name = "製票年"
        t0Year.SetTextColor(Color.Black)
        't0Year.Font.Size = 12
        t0Year.HAlignment = FPAlignment.Center
        t0Year.VAlignment = FPAlignment.Center
        Dim t0Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t0Month.Name = "製票月"
        t0Month.SetTextColor(Color.Black)
        't0Month.Font.Size = 12
        t0Month.HAlignment = FPAlignment.Center
        t0Month.VAlignment = FPAlignment.Center
        Dim t0Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t0Day.Name = "製票日"
        t0Day.SetTextColor(Color.Black)
        't0Day.Font.Size = 12
        t0Day.HAlignment = FPAlignment.Center
        t0Day.VAlignment = FPAlignment.Center
        Dim t0No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t0No.Name = "製票編號"
        t0No.SetTextColor(Color.Black)
        t0No.HAlignment = FPAlignment.Center
        t0No.VAlignment = FPAlignment.Center
        t0No.Font.Size = 11
        table0.Add(t0Year)
        table0.Add(t0Month)
        table0.Add(t0Day)
        table0.Add(t0No)

        '收款日期table
        Dim table1 As New FPTable(160, table0.Y, 70, 20, 2, 1)
        table1.SetLineColor(defaultColor)
        table1.Font.Name = "新細明體"
        table1.Font.Size = 10
        table1.SetTextColor(defaultColor)
        table1.OutlineThicken(timesOfBaseWidth)
        table1.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(1, 1).PaddingLeft = 2
        table1.Texts2D(1, 1).Text = "收款日期：　　　年　　　月　　　日"
        table1.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(2, 1).PaddingLeft = table1.Cells2D(1, 1).PaddingLeft
        table1.Texts2D(2, 1).Text = "收款編號：收 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t1Year As New FPRectangle(21, 2, 10, 5)
        'Dim t1Month As New FPRectangle(36, 2, 10, 5)
        'Dim t1Day As New FPRectangle(50, 2, 10, 5)
        'Dim t1No As New FPRectangle(34, 12, 26, 5)
        Dim t1Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t1Year.Name = "收款年"
        t1Year.SetTextColor(Color.Black)
        t1Year.HAlignment = FPAlignment.Center
        t1Year.VAlignment = FPAlignment.Center
        Dim t1Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t1Month.Name = "收款月"
        t1Month.SetTextColor(Color.Black)
        t1Month.HAlignment = FPAlignment.Center
        t1Month.VAlignment = FPAlignment.Center
        Dim t1Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t1Day.Name = "收款日"
        t1Day.SetTextColor(Color.Black)
        t1Day.HAlignment = FPAlignment.Center
        t1Day.VAlignment = FPAlignment.Center
        Dim t1No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t1No.Name = "收款編號"
        t1No.SetTextColor(Color.Black)
        t1No.HAlignment = FPAlignment.Center
        t1No.VAlignment = FPAlignment.Center
        table1.Add(t1Year)
        table1.Add(t1Month)
        table1.Add(t1Day)
        table1.Add(t1No)

        '會計科目table
        Dim table2 As New FPTable(0, table0.Y + table0.Height, 230, 12 * 7, 7, 4)
        table2.Font.Name = "新細明體"
        table2.Font.Size = 10
        table2.SetLineColor(defaultColor)
        table2.OutlineThicken(timesOfBaseWidth)
        table2.ColumnStyles(1).Width = 5
        table2.ColumnStyles(2).Width = 60
        table2.ColumnStyles(3).Width = 125
        table2.ColumnStyles(4).Width = 40
        table2.RowStyles(1).Height = 8
        table2.RowStyles(2).Height = 16
        For i As Integer = 2 To 7
            If i > 2 Then table2.RowStyles(i).Height = 12
            table2.Texts2D(i, 2).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 3).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 4).HAlignment = FPAlignment.Far
            table2.Texts2D(i, 2).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 3).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 4).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 2).Font.Size = 11
            table2.Texts2D(i, 3).Font.Size = 11
            table2.Texts2D(i, 4).Font.Size = 11
        Next
        table2.Cells2D(1, 1).ColSpan = 2   '跨二欄
        table2.Texts2D(1, 1).Text = "會　計　科　目　及　符　號"
        table2.Texts2D(1, 1).SetTextColor(defaultColor)
        table2.Texts2D(1, 3).Text = "摘　　　　　　　　　　　要"
        table2.Texts2D(1, 3).SetTextColor(defaultColor)
        table2.Texts2D(1, 4).Text = "金　　　　額"
        table2.Texts2D(1, 4).SetTextColor(defaultColor)
        table2.Texts2D(2, 1).Text = "總帳科目"
        table2.Texts2D(2, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(2, 1).SetTextColor(defaultColor)
        table2.Cells2D(3, 1).RowSpan = 5
        table2.Texts2D(3, 1).Text = "明　　細　　科　　目"
        table2.Texts2D(3, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(3, 1).SetTextColor(defaultColor)

        '沖付數table
        Dim table3 As New FPTable(0, table2.Y + table2.Height, 230, 20, 2, 5)
        table3.Font.Name = "新細明體"
        table3.Font.Size = 10
        table3.SetLineColor(defaultColor)
        table3.OutlineThicken(timesOfBaseWidth)
        table3.RowStyles(1).Height = 8
        table3.RowStyles(2).Height = 12
        table3.Texts2D(1, 1).Text = "沖　付　數"
        table3.Texts2D(1, 1).SetTextColor(defaultColor)
        table3.Texts2D(1, 2).Text = "實　收　數"
        table3.Texts2D(1, 2).SetTextColor(defaultColor)
        table3.Texts2D(1, 3).Text = "銀　行　及　帳　號"
        table3.Texts2D(1, 3).SetTextColor(defaultColor)
        table3.Texts2D(1, 4).Text = "支　票　號　碼"
        table3.Texts2D(1, 4).SetTextColor(defaultColor)
        table3.Texts2D(1, 5).Text = "受　款　人"
        table3.Texts2D(1, 5).SetTextColor(defaultColor)

        '最右方table (填入代碼用的)
        Dim table4 As New FPTable(table2.X + table2.Width, table0.Y + table0.Height, 10, table2.Height, table2.Rows, 1)
        table4.Font.Name = "新細明體"
        table4.Font.Size = 10
        table4.AllLineHide()
        table4.ColumnStyles(1).HAlignment = StringAlignment.Near

        '最下方table (決裁用)
        Dim table5 As New FPTable(0, table3.Y + table3.Height, 230, 10, 1, 5)
        table5.Font.Name = "新細明體"
        table5.Font.Size = 8
        table5.SetTextColor(defaultColor)
        For I As Integer = 1 To 5
            table5.ColumnStyles(I).HAlignment = StringAlignment.Near
        Next
        table5.AllLineHide()
        table5.Texts2D(1, 5).Text = "主辦主計人員:"
        table5.Texts2D(1, 4).Text = "主辦出納人員:"
        table5.Texts2D(1, 3).Text = "收款人:"
        table5.Texts2D(1, 2).Text = "覆核:"
        table5.Texts2D(1, 1).Text = "製票:"


        page.Add(org)
        page.Add(orgLine)
        page.Add(slip)
        page.Add(slipLine1)
        page.Add(slipLine2)
        page.Add(copykind)
        page.Add(table0)
        page.Add(table1)
        page.Add(table2)
        page.Add(table3)
        page.Add(table4)
        page.Add(table5)
        doc.AddPage(page)
        Return doc

    End Function

    '此函式產生一個空白的支出傳票報表
    Public Function GetPaySlipDoc() As FPDocument

        Dim doc As New FPDocument("支出傳票")
        doc.DefaultFont = New Font("新細明體", 12)
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.B5
        doc.DefaultPageSettings.Landscape = True
        Dim LeftMargin As Integer = 10
        doc.SetDefaultPageMargin(LeftMargin, 25, 0, 0)
        Dim page As New FPPage
        Dim defaultColor As Color = Color.Cyan
        Dim timesOfBaseWidth As Integer = 4

        '機關名稱
        Dim org As New FPText("測試農田水利會")
        org.Name = "機關名稱"
        org.Font.Name = "標楷體"
        org.Font.Size = 20
        org.SetTextColor(defaultColor)
        org.X = 78
        'org.HAlignment = FPAlignment.Center
        Dim orgSize As SizeF = org.GetSize(GraphicsUnit.Millimeter)

        '機關名稱下方的直線
        Dim orgLine As New FPLine
        orgLine.SetLineWidth(timesOfBaseWidth)
        orgLine.SetLineColor(defaultColor)
        orgLine.X1 = 78
        orgLine.Y1 = org.Y + orgSize.Height + 0.5
        orgLine.X2 = orgLine.X1 + orgSize.Width + 23
        orgLine.Y2 = orgLine.Y1

        '傳票種類
        Dim slip As New FPText("支　出　傳　票")
        slip.Y = org.Y + orgSize.Height + 3
        slip.Font.Name = "標楷體"
        slip.Font.Size = 20
        slip.SetTextColor(defaultColor)
        slip.X = 89
        'slip.HAlignment = FPAlignment.Center
        Dim slipSize As SizeF = slip.GetSize(GraphicsUnit.Millimeter)

        Dim copyKind As New FPText("", New RectangleF(104.5, slip.Y + 8, 22, 8))
        copyKind.Name = "正副本"
        copyKind.Font.Name = "標楷體"
        copyKind.Font.Size = 16
        copyKind.HAlignment = FPAlignment.Center
        copyKind.VAlignment = FPAlignment.Center

        '傳票種類下方的直線
        Dim slipLine1 As New FPLine
        slipLine1.SetLineColor(defaultColor)
        slipLine1.X1 = 89
        slipLine1.Y1 = slip.Y + slipSize.Height + 0
        slipLine1.X2 = slipLine1.X1 + slipSize.Width
        slipLine1.Y2 = slipLine1.Y1
        Dim slipLine2 As New FPLine
        slipLine2.SetLineColor(defaultColor)
        slipLine2.X1 = slipLine1.X1
        slipLine2.Y1 = slipLine1.Y1 + 1
        slipLine2.X2 = slipLine1.X2
        slipLine2.Y2 = slipLine2.Y1

        '製票日期table
        Dim table0 As New FPTable(0, org.Y + orgSize.Height * 2 / 3, 70, 20, 2, 1)
        table0.SetLineColor(defaultColor)
        table0.SetTextColor(defaultColor)
        table0.Font.Name = "新細明體"
        table0.Font.Size = 10
        table0.OutlineThicken(timesOfBaseWidth)
        table0.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(1, 1).PaddingLeft = 2
        table0.Texts2D(1, 1).Text = "製票日期：　　　年　　　月　　　日"
        table0.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(2, 1).PaddingLeft = table0.Cells2D(1, 1).PaddingLeft
        table0.Texts2D(2, 1).Text = "製票編號：支 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t0Year As New FPRectangle(21, 2, 10, 5)
        'Dim t0Month As New FPRectangle(36, 2, 10, 5)
        'Dim t0Day As New FPRectangle(50, 2, 10, 5)
        'Dim t0No As New FPRectangle(34, 12, 26, 5)
        Dim t0Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t0Year.Name = "製票年"
        t0Year.SetTextColor(Color.Black)
        't0Year.Font.Size = 12
        t0Year.HAlignment = FPAlignment.Center
        t0Year.VAlignment = FPAlignment.Center
        Dim t0Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t0Month.Name = "製票月"
        t0Month.SetTextColor(Color.Black)
        't0Month.Font.Size = 12
        t0Month.HAlignment = FPAlignment.Center
        t0Month.VAlignment = FPAlignment.Center
        Dim t0Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t0Day.Name = "製票日"
        t0Day.SetTextColor(Color.Black)
        't0Day.Font.Size = 12
        t0Day.HAlignment = FPAlignment.Center
        t0Day.VAlignment = FPAlignment.Center
        Dim t0No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t0No.Name = "製票編號"
        t0No.SetTextColor(Color.Black)
        t0No.Font.Size = 11
        t0No.HAlignment = FPAlignment.Center
        t0No.VAlignment = FPAlignment.Center
        table0.Add(t0Year)
        table0.Add(t0Month)
        table0.Add(t0Day)
        table0.Add(t0No)

        '支款日期table
        Dim table1 As New FPTable(160, table0.Y, 70, 20, 2, 1)
        table1.SetLineColor(defaultColor)
        table1.Font.Name = "新細明體"
        table1.Font.Size = 10
        table1.SetTextColor(defaultColor)
        table1.OutlineThicken(timesOfBaseWidth)
        table1.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(1, 1).PaddingLeft = 2
        table1.Texts2D(1, 1).Text = "支款日期：　　　年　　　月　　　日"
        table1.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(2, 1).PaddingLeft = table1.Cells2D(1, 1).PaddingLeft
        table1.Texts2D(2, 1).Text = "支款編號：支 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t1Year As New FPRectangle(21, 2, 10, 5)
        'Dim t1Month As New FPRectangle(36, 2, 10, 5)
        'Dim t1Day As New FPRectangle(50, 2, 10, 5)
        'Dim t1No As New FPRectangle(34, 12, 26, 5)
        Dim t1Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t1Year.Name = "支款年"
        t1Year.SetTextColor(Color.Black)
        t1Year.HAlignment = FPAlignment.Center
        t1Year.VAlignment = FPAlignment.Center
        Dim t1Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t1Month.Name = "支款月"
        t1Month.SetTextColor(Color.Black)
        t1Month.HAlignment = FPAlignment.Center
        t1Month.VAlignment = FPAlignment.Center
        Dim t1Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t1Day.Name = "支款日"
        t1Day.SetTextColor(Color.Black)
        t1Day.HAlignment = FPAlignment.Center
        t1Day.VAlignment = FPAlignment.Center
        Dim t1No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t1No.Name = "支款編號"
        t1No.SetTextColor(Color.Black)
        t1No.HAlignment = FPAlignment.Center
        t1No.VAlignment = FPAlignment.Center
        table1.Add(t1Year)
        table1.Add(t1Month)
        table1.Add(t1Day)
        table1.Add(t1No)

        '會計科目table
        Dim table2 As New FPTable(0, table0.Y + table0.Height, 230, 12 * 7, 7, 4)
        table2.Font.Name = "新細明體"
        table2.Font.Size = 10
        table2.SetLineColor(defaultColor)
        table2.OutlineThicken(timesOfBaseWidth)
        table2.ColumnStyles(1).Width = 5
        table2.ColumnStyles(2).Width = 60
        table2.ColumnStyles(3).Width = 125
        table2.ColumnStyles(4).Width = 40
        table2.RowStyles(1).Height = 8
        table2.RowStyles(2).Height = 16
        For i As Integer = 2 To 7
            If i > 2 Then table2.RowStyles(i).Height = 12
            table2.Texts2D(i, 2).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 3).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 4).HAlignment = FPAlignment.Far
            table2.Texts2D(i, 2).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 3).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 4).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 2).Font.Size = 11
            table2.Texts2D(i, 3).Font.Size = 11
            table2.Texts2D(i, 4).Font.Size = 11
        Next
        table2.Cells2D(1, 1).ColSpan = 2
        table2.Texts2D(1, 1).Text = "會　計　科　目　及　符　號"
        table2.Texts2D(1, 1).SetTextColor(defaultColor)
        table2.Texts2D(1, 3).Text = "摘　　　　　　　　　　　要"
        table2.Texts2D(1, 3).SetTextColor(defaultColor)
        table2.Texts2D(1, 4).Text = "金　　　　額"
        table2.Texts2D(1, 4).SetTextColor(defaultColor)
        table2.Texts2D(2, 1).Text = "總帳科目"
        table2.Texts2D(2, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(2, 1).SetTextColor(defaultColor)
        table2.Cells2D(3, 1).RowSpan = 5
        table2.Texts2D(3, 1).Text = "明　　細　　科　　目"
        table2.Texts2D(3, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(3, 1).SetTextColor(defaultColor)
        table2.Texts2D(2, 2).HAlignment = FPAlignment.Near
        table2.Texts2D(2, 3).HAlignment = FPAlignment.Near
        table2.Texts2D(2, 4).HAlignment = FPAlignment.Far
        table2.Texts2D(2, 2).VAlignment = FPAlignment.Center
        table2.Texts2D(2, 3).VAlignment = FPAlignment.Center
        table2.Texts2D(2, 4).VAlignment = FPAlignment.Center
        '沖收數table
        Dim table3 As New FPTable(0, table2.Y + table2.Height, 230, 20, 2, 5)
        table3.Font.Name = "新細明體"
        table3.Font.Size = 10
        table3.SetLineColor(defaultColor)
        table3.OutlineThicken(timesOfBaseWidth)
        table3.RowStyles(1).Height = 8
        table3.RowStyles(2).Height = 12
        table3.Texts2D(1, 1).Text = "沖　收　數"
        table3.Texts2D(1, 1).SetTextColor(defaultColor)
        table3.Texts2D(1, 2).Text = "實　付　數"
        table3.Texts2D(1, 2).SetTextColor(defaultColor)
        table3.Texts2D(1, 3).Text = "銀　行　及　帳　號"
        table3.Texts2D(1, 3).SetTextColor(defaultColor)
        table3.Texts2D(1, 4).Text = "支　票　號　碼"
        table3.Texts2D(1, 4).SetTextColor(defaultColor)
        table3.Texts2D(1, 5).Text = "受　款　人"
        table3.Texts2D(1, 5).SetTextColor(defaultColor)

        '最右方table (填入代碼用的)
        Dim table4 As New FPTable(table2.X + table2.Width, table0.Y + table0.Height, 10, table2.Height, table2.Rows, 1)
        table4.Font.Name = "新細明體"
        table4.Font.Size = 10
        table4.AllLineHide()
        table4.ColumnStyles(1).HAlignment = StringAlignment.Near

        '最下方table (決裁用)
        Dim table5 As New FPTable(0, table3.Y + table3.Height, 230, 10, 1, 5)
        table5.Font.Name = "新細明體"
        table5.Font.Size = 8
        table5.SetTextColor(defaultColor)
        For I As Integer = 1 To 5
            table5.ColumnStyles(I).HAlignment = StringAlignment.Near
        Next
        table5.AllLineHide()
        table5.Texts2D(1, 5).Text = "主辦主計人員:"
        table5.Texts2D(1, 4).Text = "主辦出納人員:"
        table5.Texts2D(1, 3).Text = "收(付)款人:"
        table5.Texts2D(1, 2).Text = "覆核:"
        table5.Texts2D(1, 1).Text = "製票:"


        page.Add(org)
        page.Add(orgLine)
        page.Add(slip)
        page.Add(slipLine1)
        page.Add(slipLine2)
        page.Add(copyKind)
        page.Add(table0)
        page.Add(table1)
        page.Add(table2)
        page.Add(table3)
        page.Add(table4)
        page.Add(table5)
        doc.AddPage(page)
        Return doc

    End Function




    '此函式產生一個空白的轉帳傳票報表
    Public Function GetTransSlipDoc() As FPDocument

        Dim doc As New FPDocument("轉帳傳票")
        doc.DefaultFont = New Font("新細明體", 12)
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.B5
        doc.DefaultPageSettings.Landscape = True
        Dim LeftMargin As Integer = 10
        doc.SetDefaultPageMargin(LeftMargin, 25, 0, 0)
        Dim page As New FPPage
        Dim defaultColor As Color = Color.Black
        Dim timesOfBaseWidth As Integer = 4

        '機關名稱
        Dim org As New FPText("測試農田水利會")
        org.Name = "機關名稱"
        org.Font.Name = "標楷體"
        org.Font.Size = 20
        org.SetTextColor(defaultColor)
        org.X = 78
        'org.HAlignment = FPAlignment.Center
        Dim orgSize As SizeF = org.GetSize(GraphicsUnit.Millimeter)

        '機關名稱下方的直線
        Dim orgLine As New FPLine
        orgLine.SetLineWidth(timesOfBaseWidth)
        orgLine.SetLineColor(defaultColor)
        orgLine.X1 = 78
        orgLine.Y1 = org.Y + orgSize.Height + 0.5
        orgLine.X2 = orgLine.X1 + orgSize.Width + 23
        orgLine.Y2 = orgLine.Y1

        '傳票種類
        Dim slip As New FPText("轉 帳　　　　傳 票")
        slip.Y = org.Y + orgSize.Height + 3
        slip.Font.Name = "標楷體"
        slip.Font.Size = 20
        slip.SetTextColor(defaultColor)
        slip.X = 81
        'slip.HAlignment = FPAlignment.Center
        Dim slipSize As SizeF = slip.GetSize(GraphicsUnit.Millimeter)
        'Dim transKind As New FPRectangle(104.5, slip.Y, 20, 7)
        Dim transKind As New FPText("", New RectangleF(104.5, slip.Y, 22, 8))
        transKind.Name = "轉帳種類"
        transKind.Font.Name = "標楷體"
        transKind.Font.Size = 20
        transKind.HAlignment = FPAlignment.Center
        transKind.VAlignment = FPAlignment.Center

        Dim copyKind As New FPText("", New RectangleF(104.5, slip.Y + 8, 22, 8))
        copyKind.Name = "正副本"
        copyKind.Font.Name = "標楷體"
        copyKind.Font.Size = 16
        copyKind.HAlignment = FPAlignment.Center
        copyKind.VAlignment = FPAlignment.Center

        '傳票種類下方的直線
        Dim slipLine1 As New FPLine
        slipLine1.SetLineColor(defaultColor)
        slipLine1.X1 = 81
        slipLine1.Y1 = slip.Y + slipSize.Height + 0
        slipLine1.X2 = slipLine1.X1 + slipSize.Width
        slipLine1.Y2 = slipLine1.Y1
        Dim slipLine2 As New FPLine
        slipLine2.SetLineColor(defaultColor)
        slipLine2.X1 = slipLine1.X1
        slipLine2.Y1 = slipLine1.Y1 + 1
        slipLine2.X2 = slipLine1.X2
        slipLine2.Y2 = slipLine2.Y1

        '轉帳日期table
        Dim table0 As New FPTable(0, org.Y + orgSize.Height * 2 / 3, 70, 20, 2, 1)
        table0.SetLineColor(defaultColor)
        table0.SetTextColor(defaultColor)
        table0.Font.Name = "新細明體"
        table0.Font.Size = 10
        table0.OutlineThicken(timesOfBaseWidth)
        table0.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(1, 1).PaddingLeft = 2
        table0.Texts2D(1, 1).Text = "轉帳日期：　　　年　　　月　　　日"
        table0.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table0.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table0.Cells2D(2, 1).PaddingLeft = table0.Cells2D(1, 1).PaddingLeft
        table0.Texts2D(2, 1).Text = "轉帳編號：轉 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t0Year As New FPRectangle(21, 2, 10, 5)
        'Dim t0Month As New FPRectangle(36, 2, 10, 5)
        'Dim t0Day As New FPRectangle(50, 2, 10, 5)
        'Dim t0No As New FPRectangle(34, 12, 26, 5)
        Dim t0Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t0Year.Name = "轉帳年"
        t0Year.SetTextColor(Color.Black)
        t0Year.HAlignment = FPAlignment.Center
        t0Year.VAlignment = FPAlignment.Center
        Dim t0Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t0Month.Name = "轉帳月"
        t0Month.SetTextColor(Color.Black)
        t0Month.HAlignment = FPAlignment.Center
        t0Month.VAlignment = FPAlignment.Center
        Dim t0Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t0Day.Name = "轉帳日"
        t0Day.SetTextColor(Color.Black)
        t0Day.HAlignment = FPAlignment.Center
        t0Day.VAlignment = FPAlignment.Center
        Dim t0No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t0No.Name = "轉帳編號"
        t0No.SetTextColor(Color.Black)
        t0No.HAlignment = FPAlignment.Center
        t0No.VAlignment = FPAlignment.Center
        table0.Add(t0Year)
        table0.Add(t0Month)
        table0.Add(t0Day)
        table0.Add(t0No)

        '轉帳日期table
        Dim table1 As New FPTable(160, table0.Y, 70, 20, 2, 1)
        table1.SetLineColor(defaultColor)
        table1.Font.Name = "新細明體"
        table1.Font.Size = 10
        table1.SetTextColor(defaultColor)
        table1.OutlineThicken(timesOfBaseWidth)
        table1.Texts2D(1, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(1, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(1, 1).PaddingLeft = 2
        table1.Texts2D(1, 1).Text = "轉帳日期：　　　年　　　月　　　日"
        table1.Texts2D(2, 1).HAlignment = FPAlignment.Near
        table1.Texts2D(2, 1).VAlignment = FPAlignment.Center
        table1.Cells2D(2, 1).PaddingLeft = table1.Cells2D(1, 1).PaddingLeft
        table1.Texts2D(2, 1).Text = "轉帳編號：轉 字第　　　　　　　 號"
        '以下四個方形物件是用來量測字體範圍,實際列印應該將之隱藏
        'Dim t1Year As New FPRectangle(21, 2, 10, 5)
        'Dim t1Month As New FPRectangle(36, 2, 10, 5)
        'Dim t1Day As New FPRectangle(50, 2, 10, 5)
        'Dim t1No As New FPRectangle(34, 12, 26, 5)
        Dim t1Year As New FPText("", New RectangleF(21, 2.5, 10, 5))
        t1Year.Name = "轉帳年"
        t1Year.SetTextColor(Color.Black)
        t1Year.HAlignment = FPAlignment.Center
        t1Year.VAlignment = FPAlignment.Center
        Dim t1Month As New FPText("", New RectangleF(36, 2.5, 10, 5))
        t1Month.Name = "轉帳月"
        t1Month.SetTextColor(Color.Black)
        t1Month.HAlignment = FPAlignment.Center
        t1Month.VAlignment = FPAlignment.Center
        Dim t1Day As New FPText("", New RectangleF(50, 2.5, 10, 5))
        t1Day.Name = "轉帳日"
        t1Day.SetTextColor(Color.Black)
        t1Day.HAlignment = FPAlignment.Center
        t1Day.VAlignment = FPAlignment.Center
        Dim t1No As New FPText("", New RectangleF(34, 12.5, 26, 5))
        t1No.Name = "轉帳編號"
        t1No.SetTextColor(Color.Black)
        t1No.HAlignment = FPAlignment.Center
        t1No.VAlignment = FPAlignment.Center
        table1.Add(t1Year)
        table1.Add(t1Month)
        table1.Add(t1Day)
        table1.Add(t1No)

        '會計科目table
        Dim table2 As New FPTable(0, table0.Y + table0.Height, 230, 12 * 7, 7, 4)
        table2.Font.Name = "新細明體"
        table2.Font.Size = 10
        table2.SetLineColor(defaultColor)
        table2.OutlineThicken(timesOfBaseWidth)
        table2.ColumnStyles(1).Width = 5
        table2.ColumnStyles(2).Width = 60
        table2.ColumnStyles(3).Width = 125
        table2.ColumnStyles(4).Width = 40
        table2.RowStyles(1).Height = 8
        table2.RowStyles(2).Height = 16
        'TABLE2.ColumnStyles(2).HAlignment=StringAlignment.Far整欄右靠
        For i As Integer = 2 To 7
            If i > 2 Then table2.RowStyles(i).Height = 12
            table2.Texts2D(i, 2).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 3).HAlignment = FPAlignment.Near
            table2.Texts2D(i, 4).HAlignment = FPAlignment.Far
            table2.Texts2D(i, 2).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 3).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 4).VAlignment = FPAlignment.Center
            table2.Texts2D(i, 2).Font.Size = 11
            table2.Texts2D(i, 3).Font.Size = 11
            table2.Texts2D(i, 4).Font.Size = 11
        Next
        table2.Cells2D(1, 1).ColSpan = 2
        table2.Texts2D(1, 1).Text = "會　計　科　目　及　符　號"
        table2.Texts2D(1, 1).SetTextColor(defaultColor)
        table2.Texts2D(1, 3).Text = "摘　　　　　　　　　　　要"
        table2.Texts2D(1, 3).SetTextColor(defaultColor)
        table2.Texts2D(1, 4).Text = "金　　　　額"
        table2.Texts2D(1, 4).SetTextColor(defaultColor)
        table2.Texts2D(2, 1).Text = "總帳科目"
        table2.Texts2D(2, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(2, 1).SetTextColor(defaultColor)
        table2.Cells2D(3, 1).RowSpan = 5
        table2.Texts2D(3, 1).Text = "明　　細　　科　　目"
        table2.Texts2D(3, 1).StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
        table2.Texts2D(3, 1).SetTextColor(defaultColor)


        '最右方table (填入代碼用的)
        Dim table4 As New FPTable(table2.X + table2.Width, table0.Y + table0.Height, 10, table2.Height, table2.Rows, 1)
        table4.Font.Name = "新細明體"
        table4.Font.Size = 10
        table4.AllLineHide()
        table4.ColumnStyles(1).HAlignment = StringAlignment.Near

        '最下方table (決裁用)
        Dim table5 As New FPTable(0, table2.Y + table2.Height + 10, 230, 10, 1, 5)   '隔一公分印決裁行
        table5.Font.Name = "新細明體"
        table5.Font.Size = 8
        table5.SetTextColor(defaultColor)
        For I As Integer = 1 To 5
            table5.ColumnStyles(I).HAlignment = StringAlignment.Near
        Next
        table5.AllLineHide()
        table5.Texts2D(1, 5).Text = "主辦主計人員:"
        table5.Texts2D(1, 4).Text = "主辦出納人員:"
        table5.Texts2D(1, 3).Text = "收/付款人:"
        table5.Texts2D(1, 2).Text = "覆核:"
        table5.Texts2D(1, 1).Text = "製票:"

        page.Add(org)
        page.Add(orgLine)
        page.Add(slip)
        page.Add(transKind)
        page.Add(copyKind)
        page.Add(slipLine1)
        page.Add(slipLine2)
        page.Add(table0)
        page.Add(table1)
        page.Add(table2)
        page.Add(table4)
        page.Add(table5)
        doc.AddPage(page)
        Return doc

    End Function


End Module
