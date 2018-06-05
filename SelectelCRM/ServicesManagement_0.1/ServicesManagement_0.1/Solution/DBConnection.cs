using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Solution
{
    //Работа с базой данных: получение, вставка, обновление, удаление информации
    class DBConnection
    {
        //Строка подключения
        static public string connection_string = @"DataBase = servicemandb; Data Source = 206.189.10.249; charset = utf8; UserID = root; Password = qweasdiop;";
        //Объект соединения
        static public MySqlConnection msConnect = new MySqlConnection();
        //Объект MySQL команды
        static public MySqlCommand msCommand = new MySqlCommand();
        //Объект адаптера данных
        static public MySqlDataAdapter msDataAdapter = new MySqlDataAdapter();
        
        //Объекты для хранения данных из таблиц БД
        static public DataTable dtRequests = new DataTable();
        static public DataTable dtFoo = new DataTable();
        static public DataTable dtServicesInRequest = new DataTable();
        static public DataTable dtClients = new DataTable();
        static public DataTable dtManagers = new DataTable();
        static public DataTable dtCountries = new DataTable();
        static public DataTable dtCities = new DataTable();
        static public DataTable dtServices = new DataTable();
        static public DataTable dtServicesInContract = new DataTable();
        static public DataTable dtServicesInContractByMonth = new DataTable();
        static public DataTable dtServicesGroups = new DataTable();
        static public DataTable dtServicesInGroup = new DataTable();
        static public DataTable dtSellings = new DataTable();
        static public DataTable dtDocs = new DataTable();
        static public DataTable dtIdInvoice = new DataTable();
        static public DataTable dtNotifications = new DataTable();
        static public DataTable dtSellingsYears = new DataTable();
        static public DataTable dtSellingsByYear = new DataTable();
        static public DataTable dtTmp = new DataTable();

        static public string id = null;
        static public string login = null;

        //Установление соединения с базой данных
        static public bool Connect()
        {
            try
            {
                msConnect = new MySqlConnection(connection_string);
                msConnect.Open();
                msCommand = new MySqlCommand();
                msCommand.Connection = msConnect;
                msDataAdapter = new MySqlDataAdapter();
                //MessageBox.Show("Success!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Авторизация пользователя по логину и паролю
        static public void Authorization(string _login, string password)
        {
            msCommand.CommandText = "SELECT id FROM managers WHERE login = '" + _login + "' AND password = '" + password + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                id = result.ToString();
                login = _login;
            }
            else
            {
                id = login = null;
            }
        }

        //Получение информации о заявках и договорах, смена статуса завершенных договоров
        static public void GetRequests()
        {
            msCommand.CommandText = "SELECT request_num, status FROM requests_and_contracts;";
            dtFoo.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtFoo);

            foreach (DataRow row in dtFoo.AsEnumerable())
            {
                if (row[1].ToString() != "Завершен")
                {
                    msCommand.CommandText = "SELECT MAX(s.date_end) FROM services_in_request sir, sales s WHERE sir.request_num = '" + row[0].ToString() + "' AND s.num_service_in_request = sir.num_service_in_request;";
                    object result = msCommand.ExecuteScalar(); //Результат запроса
                    if (result != null && result.ToString() != "")
                    {
                        if (Convert.ToDateTime(result.ToString()) <= DateTime.Now)
                        {
                            ChangeStatus(row[0].ToString(), "Завершен");
                        }
                    }
                }
            }
            //dtRequests.Columns.Clear();
            dtRequests.Clear();
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус`, rc.client_num, rc.manager_num, rc.date_end as `Дата завершения`, c.email FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num ORDER BY rc.request_num DESC";
            
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);

        }

        //Получение информации об услугах в конкретной заявке
        static public void GetServicesInRequest(string request_num)
        {
            msCommand.CommandText = "SELECT sr.num_service_in_request as `Номер услуги в заявке`, sr.request_num as `Номер заявки`, g.name as `Группа услуг`, s.name as `Услуга`, s.cost as `Цена`, sr.date_start as `Желаемая дата начала`, sr.date_end as `Желаемая дата окончания`, sr.cost as `Стоимость` FROM services_in_request sr, services_groups g, services s WHERE s.group_num = g.id AND sr.service_num = s.id AND sr.request_num = '" + request_num + "';";
            dtServicesInRequest.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServicesInRequest);
        }

        //Получение информации о всех клиентах
        static public void GetClients()
        {
            msCommand.CommandText = "SELECT c.id as `ID`, c.name as `Название`, c.contact_person as `Контактное лицо`, c.phone as `Телефон`, co.name as `Страна`, ci.name as `Город`, c.adress as `Адрес`, c.email as `E-mail`, c.bank_account as `Банк. счет`, c.INN as `ИНН`, c.country_code, c.city_code FROM clients c, countries co, cities ci WHERE c.country_code = co.id AND c.city_code = ci.id;";
            dtClients.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtClients);
        }

        //Получение информации о всех менеджерах
        static public void GetManagers()
        {
            msCommand.CommandText = "SELECT id as `ID`, full_name as `ФИО`, adress as `Адрес`, phone as `Телефон`, date_of_birth as `Дата рождения`, date_start_work as `Дата приема на работу`, login as `Логин`, password as `Пароль` FROM managers;";
            dtManagers.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtManagers);
        }

        //Создание новой заявки
        static public void NewRequest(string clientID, string managerID, string date, string status)
        {
            msCommand.CommandText = "INSERT INTO requests_and_contracts VALUES (NULL, NULL, '" + clientID + "', '" + managerID + "', '" + date + "', NULL, NULL, '" + status + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новая заявка успешно создана!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получение информации о странах из справочника
        static public void GetCountries()
        {
            msCommand.CommandText = "SELECT id as `ID`, name as `Название` FROM countries;";
            dtCountries.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtCountries);
        }
        
        //Получение информации о городах из справочника
        static public void GetCities()
        {
            msCommand.CommandText = "SELECT id as `ID`, name as `Название` FROM cities;";
            dtCities.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtCities);
        }

        //Создание нового клиента
        static public void NewClient(string name, string contact_person, string phone, string country, string city, string adress, string email, string bank_account, string inn)
        {
            msCommand.CommandText = "INSERT INTO clients VALUES (NULL, '" + name + "', '" + contact_person + "', '" + phone + "', '" + country + "', '" + city + "', '" + adress + "', '" + email + "', '" + bank_account + "', '" + inn + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новый клиент успешно создан!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получние информации о всех услугах
        static public void GetServices()
        {
            msCommand.CommandText = "SELECT * FROM services;";
            dtServices.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServices);
        }

        //Добавление новой услуги в заявку
        static public void NewServiceInRequest(string serviceID, string date_start, string date_end, string cost)
        {
            msCommand.CommandText = "INSERT INTO services_in_request VALUES (NULL, '" + Form1.requestNum + "', '" + serviceID + "', '" + cost + "', '" + date_start + "', '" + date_end + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Создание договора к заявке, смена статуса на "В обработке"
        static public void NewContract(string contract_num)
        {
            msCommand.CommandText = "UPDATE requests_and_contracts SET contract_num = '" + contract_num + "', contract_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', status = 'В обработке' WHERE request_num = '" + Form1.requestNum + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Договор уcпешно создан!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Добавление услуги в договор
        static public void NewServiceInContract(string num_service_in_request, string cost, string date_start, string date_end)
        {
            msCommand.CommandText = "INSERT INTO sales VALUES (NULL, '" + num_service_in_request + "', '" + cost + "', NULL, '" + date_start + "', '" + date_end + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получение информации об услугах в договоре
        static public void GetServicesInContract(string request_num)
        {
            msCommand.CommandText = "SELECT s.id as `ID`, s.num_service_in_request as `Номер услуги в заявке`, srv.name as `Название услуги`, s.cost as `Стоимость`, s.date_start as `Дата начала`, s.date_end as `Дата окончания` FROM sales s, services srv, services_in_request sir WHERE sir.request_num = '" + request_num + "' AND s.num_service_in_request = sir.num_service_in_request AND srv.id = sir.service_num;";

            dtServicesInContract.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServicesInContract);
        }

        //Получение информации об услугах в договоре за конкретный месяц
        static public void GetServicesInContractByMonth(string request_num, string date)
        {
            DBConnection.msCommand.CommandText = "SELECT COUNT(s.id) as `cnt`, srv.name as `Название услуги`, srv.cost as `Стоимость`, s.date_start as `Дата начала`, s.date_end as `Дата окончания` FROM sales s, services srv, services_in_request sir WHERE sir.request_num = '" + request_num + "' AND s.num_service_in_request = sir.num_service_in_request AND srv.id = sir.service_num AND CAST(CONCAT(DATE_FORMAT('" + date + "', '%y-%m'), '-01') AS DATE) BETWEEN CAST(CONCAT(DATE_FORMAT(s.date_start, '%y-%m'), '-01') AS DATE) AND s.date_end GROUP BY srv.cost, srv.name, s.date_start, s.date_end;"; //('" + month + "' >= EXTRACT(MONTH FROM s.date_start) OR '" + month + "' <= EXTRACT(MONTH FROM s.date_end)) AND ('" + year + "' >= EXTRACT(YEAR FROM s.date_start) OR '" + year + "' <= EXTRACT(YEAR FROM s.date_end))
            dtServicesInContractByMonth.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServicesInContractByMonth);
        }

        //Поиск заявок и договоров по номеру заявки
        static public void SearchRequestNumber(string request_num)
        {
            msCommand.CommandText = "SELECT request_num FROM requests_and_contracts WHERE request_num = '" + request_num + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Запись не найдена!");
            }
        }

        //Отбор заявок и договоров по дате оформления заявки
        static public void FilterRequestDate(string date1, string date2)
        {
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус` FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num AND rc.request_date BETWEEN '" + date1 + "' AND '" + date2 + "';"; 

            dtRequests.Clear();
            
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);
        }

        //Отбор договор по дате оформления
        static public void FilterContractDate(string date1, string date2)
        {
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус` FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num AND rc.contract_date BETWEEN '" + date1 + "' AND '" + date2 + "';";

            dtRequests.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);
        }

        //Отбор заявок и договоров по клиенту
        static public void FilterClient(string clientID)
        {
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус` FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num AND c.id = '" + clientID + "';";

            dtRequests.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);
        }

        //Отбор заявок и договоров по статусу
        static public void FilterStatus(string status)
        {
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус` FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num AND rc.status = '" + status + "';";

            dtRequests.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);
        }

        //Отбор  заявок и договоров по менеджеру
        static public void FilterManager(string managerID)
        {
            msCommand.CommandText = "SELECT rc.request_num as `Номер заявки`, rc.contract_num as `Номер договора`, c.name as `Клиент`, c.contact_person as `Контактное лицо`, m.full_name as `Менеджер`, rc.request_date as `Дата заявки`, rc.contract_date as `Дата контракта`, rc.status as `Статус` FROM requests_and_contracts rc, clients c, managers m WHERE c.id = rc.client_num AND m.id = rc.manager_num AND m.id = '" + managerID + "';";

            dtRequests.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtRequests);
        }

        //Получение информации о группах услуг
        static public void GetServicesGroups()
        {
            msCommand.CommandText = "SELECT id as `ID`, name as `Наименование` FROM services_groups;";

            dtServicesGroups.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServicesGroups);
        }

        //Получение информации об услугах в конкретной группе
        static public void GetServicesInGroup(string groupID)
        {
            msCommand.CommandText = "SELECT s.id, s.group_num as `Группа услуг`, s.name as `Название`, s.info as `Информация`, s.cost as `Стоимость` FROM services s, services_groups sg WHERE sg.id = s.group_num AND sg.id = '" + groupID + "';";

            dtServicesInGroup.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtServicesInGroup);
        }

        //Редактирование информации о клиенте
        static public void EditClient(string clientID, string name, string contact_person, string phone, string country_code, string city_code, string adress, string email, string bank_account, string inn)
        {
            msCommand.CommandText = "UPDATE clients SET name = '" + name + "', contact_person = '" + contact_person + "', phone = '" + phone + "', country_code = '" + country_code + "', city_code = '" + city_code + "', adress = '" + adress + "', email = '" + email + "', bank_account = '" + bank_account + "', INN = '" + inn + "' WHERE id = '" + clientID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Клиент успешно обновлен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Создание нового менеджера, проверка на дублирование
        static public void NewManager(string full_name, string adress, string phone, string date_of_birth, string date_start_work, string login, string password)
        {
            msCommand.CommandText = "SELECT login FROM managers WHERE login = '" + login + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Ошибка! Менеджер с таким логином уже зарегистрирован!");
                return;
            }

            msCommand.CommandText = "INSERT INTO managers VALUES (NULL, '" + full_name + "', '" + adress + "', '" + phone + "', '" + date_of_birth + "', '" + date_start_work + "', '" + login + "', '" + password + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новый менеджер успешно создан!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование менеджера, проверка на дублирование
        static public void EditManager(string managerID, string full_name, string adress, string phone, string date_of_birth, string date_start_work, string login, string password)
        {
            msCommand.CommandText = "SELECT id FROM managers WHERE login = '" + login + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                if (result.ToString() != managerID)
                {
                    MessageBox.Show("Ошибка! Менеджер с таким логином уже зарегистрирован!");
                    return;
                }
            }

            msCommand.CommandText = "UPDATE managers SET full_name = '" + full_name + "', adress = '" + adress + "', phone = '" + phone + "', date_of_birth = '" + date_of_birth + "', date_start_work = '" + date_start_work + "', login = '" + login + "', password = '" + password + "' WHERE id = '" + managerID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Менеджер успешно обновлен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Добавление новой группы услуг
        static public void NewServiceGroup(string name)
        {
            msCommand.CommandText = "INSERT INTO services_groups VALUES (NULL, '" + name + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новая группа услуг успешно создана!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование группы услуг
        static public void EditServiceGroup(string serviceGroupID, string serviceGroupName)
        {
            msCommand.CommandText = "UPDATE services_groups SET name = '" + serviceGroupName + "' WHERE id = '" + serviceGroupID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Группа услуг успешно обновлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Добавление услуги в конкретную группу
        static public void NewServiceInGroup(string serviceGroupNum, string name, string info, string cost)
        {
            msCommand.CommandText = "INSERT INTO services VALUES (NULL, '" + serviceGroupNum + "', '" + name + "', '" + info + "', '" + cost + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новая услуга успешно создана!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование услуги в определенной группе
        static public void EditServiceInGroup(string serviceID, string name, string info, string cost)
        {
            msCommand.CommandText = "UPDATE services SET name = '" + name + "', info = '" + info + "', cost = '" + cost + "' WHERE id = '" + serviceID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно обновлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Отбор клиентов по стране
        static public void FilterCountry(string countryID)
        {
            msCommand.CommandText = "SELECT c.id as `ID`, c.name as `Название`, c.contact_person as `Контактное лицо`, c.phone as `Телефон`, co.name as `Страна`, ci.name as `Город`, c.adress as `Адрес`, c.email as `E-mail`, c.bank_account as `Банк. счет`, c.INN as `ИНН`, c.country_code, c.city_code FROM clients c, countries co, cities ci WHERE c.country_code = co.id AND c.city_code = ci.id AND co.id = '" + countryID + "';";

            dtClients.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtClients);
        }

        //Отбор клиентов по городу
        static public void FilterCity(string cityID)
        {
            msCommand.CommandText = "SELECT c.id as `ID`, c.name as `Название`, c.contact_person as `Контактное лицо`, c.phone as `Телефон`, co.name as `Страна`, ci.name as `Город`, c.adress as `Адрес`, c.email as `E-mail`, c.bank_account as `Банк. счет`, c.INN as `ИНН`, c.country_code, c.city_code FROM clients c, countries co, cities ci WHERE c.country_code = co.id AND c.city_code = ci.id AND ci.id = '" + cityID + "';";

            dtClients.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtClients);
        }

        //Отбор менеджеров по дате приема на работу
        static public void FilterStartWorkDate(string date1, string date2)
        {
            msCommand.CommandText = "SELECT id as `ID`, full_name as `ФИО`, adress as `Адрес`, phone as `Телефон`, date_of_birth as `Дата рождения`, date_start_work as `Дата приема на работу`, login as `Логин`, password as `Пароль` FROM managers WHERE date_start_work BETWEEN '" + date1 + "' AND '" + date2 + "';";
            dtManagers.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtManagers);
        }

        //Добавление новой страны в справочник, проверка на дублирование
        static public void NewCountry(string country_code, string countryName)
        {
            msCommand.CommandText = "SELECT id FROM countries WHERE id = '" + country_code + "' OR name = '" + countryName + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружено дублирование! Запись не была добавлена.");
                return;
            }

            msCommand.CommandText = "INSERT INTO countries VALUES ('" + country_code + "', '" + countryName + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новая страна успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование страны в справочнике с условием, что не обнаружены внешние зависимости
        static public void EditCountry(string countryID, string countryName, string newCountryID, string newCountryName)
        {
            object result; //Переменная для хранения результата запроса
            if (countryID != newCountryID)
            {
                msCommand.CommandText = "SELECT id FROM clients WHERE country_code = '" + countryID + "';";
                result = msCommand.ExecuteScalar(); //Результат запроса
                if (result != null)
                {
                    MessageBox.Show("Обнаружены внешние зависимости! Запись не была обновлена.");
                    return;
                }
            }

            msCommand.CommandText = "SELECT id FROM countries WHERE id = '" + newCountryID + "';";
            result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                if (result.ToString() != countryID)
                {
                    MessageBox.Show("Обнаружено дублирование! Запись не была добавлена.");
                    return;
                }
            }
            msCommand.CommandText = "SELECT name FROM countries WHERE name = '" + newCountryName + "';";
            result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                if (result.ToString() != countryName)
                {
                    MessageBox.Show("Обнаружено дублирование! Запись не была добавлена.");
                    return;
                }
            }

            msCommand.CommandText = "UPDATE countries SET id = '" + newCountryID + "', name = '" + newCountryName + "' WHERE id = '" + countryID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Страна успешно обновлена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Добавление города в справочник, проверка на дублирование
        static public void NewCity(string cityCode, string cityName)
        {
            msCommand.CommandText = "SELECT id FROM cities WHERE id = '" + cityCode + "' OR name = '" + cityName + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружено дублирование! Запись не была добавлена.");
                return;
            }

            msCommand.CommandText = "INSERT INTO cities VALUES ('" + cityCode + "', '" + cityName + "');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новый город успешно добавлен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование города в справочнике с условием, что не обнаружены внешние зависимости
        static public void EditCity(string cityID, string cityName, string newCityID, string newCityName)
        {
            object result; //Переменная для хранения результата запроса
            if (cityID != newCityID)
            {
                msCommand.CommandText = "SELECT id FROM clients WHERE city_code = '" + cityID + "';";
                result = msCommand.ExecuteScalar(); //Результат запроса
                if (result != null)
                {
                     MessageBox.Show("Обнаружены внешние зависимости! Запись не была обновлена.");
                     return;
                }
            }
            msCommand.CommandText = "SELECT id FROM cities WHERE id = '" + newCityID + "';";
            result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                if (result.ToString() != cityID)
                {
                    MessageBox.Show("Обнаружено дублирование! Запись не была обновлена.");
                    return;
                }
            }
            msCommand.CommandText = "SELECT name FROM cities WHERE name = '" + newCityName + "';";
            result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                if (result.ToString() != cityName)
                {
                    MessageBox.Show("Обнаружено дублирование! Запись не была обновлена.");
                    return;
                }
            }

            msCommand.CommandText = "UPDATE cities SET id = '" + newCityID + "', name = '" + newCityName + "' WHERE id = '" + cityID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Город успешно обновлен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление услуги из договора
        static public void DeleteSale(string saleID)
        {
            msCommand.CommandText = "DELETE FROM sales WHERE id = '" + saleID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно удалена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление услуги из заявки с условием, что не обнаружены внешние зависимости
        static public void DeleteServiceInRequest(string serviceID)
        {
            msCommand.CommandText = "SELECT id FROM sales WHERE num_service_in_request = '" + serviceID + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }
            msCommand.CommandText = "DELETE FROM services_in_request WHERE num_service_in_request = '" + serviceID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно удалена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование заявки
        static public void EditRequest(string requestNum, string clientID, string managerID, string dateRequest, string dateContract)
        {
            msCommand.CommandText = "UPDATE requests_and_contracts SET manager_num = '" + managerID + "', client_num = '" + clientID + "', request_date = '" + dateRequest + "', contract_date = '" + dateContract + "' WHERE request_num = '" + requestNum + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Заявка/Договор успешно обновлены!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление заявки/договора с условием, что не обнаружены внешние зависимости
        static public bool DeleteRequest(string requestNum)
        {
            msCommand.CommandText = "SELECT request_num FROM services_in_request WHERE request_num = '" + requestNum + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return false;
            }
            msCommand.CommandText = "SELECT request_num FROM docs WHERE request_num = '" + requestNum + "';";
            result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return false;
            }
            msCommand.CommandText = "DELETE FROM requests_and_contracts WHERE request_num = '" + requestNum + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Заявка/договор успешно удалены!");
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка!");
                return false;
            }
        }

        //Удаление клиена с условием, что не обнаружены внешние зависимости
        static public void DeleteClient(string clientID)
        {
            msCommand.CommandText = "SELECT client_num FROM requests_and_contracts WHERE client_num = '" + clientID + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }
            msCommand.CommandText = "DELETE FROM clients WHERE id = '" + clientID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Клиент успешно удален!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление менеджера с условием, что не обнаружены внешние зависимости
        static public void DeleteManager(string managerID)
        {
            msCommand.CommandText = "SELECT manager_num FROM requests_and_contracts WHERE manager_num = '" + managerID + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }
            msCommand.CommandText = "DELETE FROM managers WHERE id = '" + managerID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Менеджер успешно удален!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление группы услуг с условием, что не обнаружены внешние зависимости
        static public void DeleteServiceGroup(string serviceGroupID)
        {
            msCommand.CommandText = "SELECT group_num FROM services WHERE group_num = '" + serviceGroupID + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }
            msCommand.CommandText = "DELETE FROM services_groups WHERE id = '" + serviceGroupID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Группа услуг успешно удалена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление услуги с условием, что не обнаружены внешние зависимости
        static public void DeleteService(string serviceID)
        {
            msCommand.CommandText = "SELECT service_num FROM services_in_request WHERE service_num = '" + serviceID + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }
            msCommand.CommandText = "DELETE FROM services WHERE id = '" + serviceID + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Услуга успешно удалена!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получение информации об объемах продаж за все время, сгруппированных по каждому месяцу
        static public void GetSellings()
        {
            msCommand.CommandText = "SELECT EXTRACT(MONTH FROM s.date_end) AS `month`, SUM(s.cost) AS `sum` FROM sales s GROUP BY `month`;"; //"SELECT EXTRACT(MONTH FROM s.date_end) as `month`, SUM(s.cost) as `sum` FROM requests_and_contracts rc, services_in_request sin, sales s WHERE sin.request_num = rc.request_num AND s.num_service_in_request = sin.num_service_in_request GROUP BY `month`;";

            dtSellings.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtSellings);
        }

        //Получение информации об объемах продаж за определенный год, сгруппированных по каждому месяцу
        static public void GetSellingsByYear(string year)
        {
            msCommand.CommandText = "SELECT EXTRACT(MONTH FROM s.date_end) AS `month`, SUM(s.cost) AS `sum` FROM sales s WHERE EXTRACT(YEAR FROM s.date_end) = '" + year + "' GROUP BY `month`;"; //"SELECT EXTRACT(MONTH FROM s.date_end) as `month`, SUM(s.cost) as `sum` FROM requests_and_contracts rc, services_in_request sin, sales s WHERE sin.request_num = rc.request_num AND s.num_service_in_request = sin.num_service_in_request GROUP BY `month`;";

            dtSellingsByYear.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtSellingsByYear);
        }

        //Получение информации о годах,  которых были продажи
        static public void GetSellingsYears()
        {
            msCommand.CommandText = "SELECT EXTRACT(YEAR FROM s.date_end) as `year` FROM sales s GROUP BY `year`;";

            dtSellingsYears.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtSellingsYears);
        }

        //Создание нового документа (договор, приложение к договору, счет-фактура)
        static public void NewDoc(string request_num, string name, string date_create)
        {
            msCommand.CommandText = "INSERT INTO docs VALUES (NULL, '" + request_num + "', '" + name + "', '" + date_create + "', NULL);";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Новый документ успешно создан!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Установление текущей даты подписания у документа
        static public void SignDoc(string doc_num)
        {
            msCommand.CommandText = "UPDATE docs SET date_sign = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE id = '" + doc_num + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Документ успешно подписан и сохранен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Смена статуса заявки/договора
        static public void ChangeStatus(string request_num, string status)
        {
            msCommand.CommandText = "UPDATE requests_and_contracts SET status = '" + status + "' WHERE request_num = '" + request_num + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Статус успешно изменен!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получение информации о документах связанных с конкретной заявкой/договором
        static public void GetDocs(string request_num)
        {
            msCommand.CommandText = "SELECT id AS `Номер`, name AS `Название`, date_create AS `Дата вступления в силу`, date_sign AS `Дата подписания` FROM docs WHERE request_num = '" + request_num + "';";

            dtDocs.Clear();

            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtDocs);
        }

        //Установление даты окончания действия договора
        static public void SetContractDateEnd(string request_num, string date_end)
        {
            msCommand.CommandText = "UPDATE requests_and_contracts SET date_end = '" + date_end + "' WHERE request_num = '" + request_num + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Получение информации об email уведомлениях для конкретной заявки
        static public void GetNotifications(string request_num)
        {
            msCommand.CommandText = "SELECT srv.name AS `Название услуги`, n.days_before AS `Дней до завершения услуги`, n.text AS `Текст уведомления`, n.status AS `Статус`, n.id FROM notifications n, sales s, services_in_request sir, services srv WHERE n.sale_id = s.id AND s.num_service_in_request = sir.num_service_in_request AND sir.service_num = srv.id AND sir.request_num = '" + request_num + "';";

            dtNotifications.Clear();
            msDataAdapter = new MySqlDataAdapter(msCommand);
            msDataAdapter.Fill(dtNotifications);
        }

        //Добавление нового email уведомления к действующей услуге
        static public void NewNotification(string sale_id, string days_before, string text)
        {
            msCommand.CommandText = "INSERT INTO notifications VALUES (NULL, '" + sale_id + "', '" + days_before + "', '" + text + "', 'Новое');";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Новое уведомление успешно создано!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Редактирование уведомления
        static public void EditNotification(string notification_id, string days_before, string text, string status)
        {
            msCommand.CommandText = "UPDATE notifications SET days_before = '" + days_before + "', text = '" + text + "', status = '" + status + "' WHERE id = '" + notification_id + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление уведомления
        static public void DeleteNotification(string notification_id)
        {
            msCommand.CommandText = "DELETE FROM notifications WHERE id = '" + notification_id + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление страны из справочника с условием, что не обнаружены внешние зависимости
        static public void DeleteCountry(string country_id)
        {
            msCommand.CommandText = "SELECT id FROM clients WHERE country_code = '" + country_id + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }

            msCommand.CommandText = "DELETE FROM countries WHERE id = '" + country_id + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }

        //Удаление города из справочника с условием, что не обнаружены внешние зависимости
        static public void DeleteCity(string city_id)
        {
            msCommand.CommandText = "SELECT id FROM clients WHERE city_code = '" + city_id + "';";
            object result = msCommand.ExecuteScalar(); //Результат запроса
            if (result != null)
            {
                MessageBox.Show("Обнаружены внешние зависимости. Удаление невозможно.");
                return;
            }

            msCommand.CommandText = "DELETE FROM cities WHERE id = '" + city_id + "';";
            if (msCommand.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
