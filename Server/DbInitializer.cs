using SingleGridFormWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleGridFormWebApp.Server
{
    public class DbInitializer
    {
        public static void Initialize(FormContext context)
        {
            context.Database.EnsureCreated();

            if (context.Forms.Count() == 0)
            {
                var columns1 = new Column[]
               {
            new Column{Name="Hight"},
            new Column{Name="Weight"}
               };


                var form1 = new Form()
                {
                    Name = "Person",
                    Columns = new List<Column>(),
                    Rows = new List<Row>()
                };

                var rows1 = new Row[]
                {
                //new Row(){Form = form1},
                //new Row(){Form = form1},
                //new Row(){Form = form1}
                };

                form1.Columns = columns1;
                form1.Rows = rows1.ToList();

                foreach (var c in columns1)
                {
                    context.Columns.Add(c);
                }
                foreach (var r in rows1)
                {
                    context.Rows.Add(r);
                }

                context.Forms.Add(form1);

                foreach (var row in form1.Rows)
                {
                    foreach (var col in form1.Columns)
                    {
                        context.RowDataSet.Add(new RowData() { Form = form1, Row = row, Column = col, Value = "170" });
                    }
                }
                context.SaveChanges();

            }
        }
    }
}
