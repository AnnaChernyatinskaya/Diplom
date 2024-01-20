using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestApp
{
    public partial class Request
    {
        DateTime date = DateTime.Now;

        public string RequestStatusString
        {
            get
            {
                if (StatusId == 1)
                    return "Отправлена";
                else if (StatusId == 2)
                    return "В работе";
                else if (StatusId == 3)
                    return "Закрыта";
                else return "";
            }
        }

        public string RequestTemaString
        {
            get
            {
                if (TemaId == 1)
                    return "Оборудование";
                else if (TemaId == 2)
                    return "Программное обеспечение";
                else if (TemaId == 3)
                    return "Лицензия";
                else if (TemaId == 4)
                    return "Документация";
                else return "";
            }
        }

        public string RequestImportString
        {
            get
            {
                if (ImportanceId == 1)
                    return "Низкая";
                else if (ImportanceId == 2)
                    return "Средняя";
                else if (ImportanceId == 3)
                    return "Высокая";
                else return "";
            }
        }

        public string DateStartRequest
        {
            get
            {
                if (DateStart == null)
                {
                    return string.Empty;
                }
                else
                {
                    return DateStart.Date.ToShortDateString();
                }
            }
        }

        public string DateFinishRequest
        {
            get
            {
                if (DateFinish == null)
                {
                    return string.Empty;
                }
                else
                {
                    return DateFinish.Date.ToShortDateString();
                }
            }
        }

        public string BackColor
        {
            get
            {
                if (StatusId == 1)
                    return "#FF6347";
                else if (StatusId == 2)
                    return "#FFFF00";
                else if (StatusId == 3)
                    return "#7FFF00";
                else
                    return "white";
            }
        }

        public string BackColorDate
        {
            get
            {
                if (DateFinish < date && StatusId != 3)
                    return "#FF6347";
                else
                    return "white";
            }
        }
    }
}
