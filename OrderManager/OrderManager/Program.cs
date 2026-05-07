const int deliveryDays = 3;

PrintHeader();

string product = ReadNonEmptyLine( "Введите название товара: " );
int quantity = GetQuantity();
string customerName = ReadNonEmptyLine( "Введите ваше имя: " );
string address = ReadNonEmptyLine( "Введите адрес доставки: " );

bool isOrderConfirmed = ConfirmOrder( customerName, product, quantity, address );

if ( isOrderConfirmed )
{
    DateTime deliveryDate = DateTime.Today.AddDays( deliveryDays );
    ShowSuccessMessage( customerName, product, quantity, address, deliveryDate );
}
else
{
    ShowCancelMessage( customerName, product );
}

Console.WriteLine( "\nНажмите любую клавишу для выхода..." );
Console.ReadKey();

int GetQuantity()
{
    int quantity;
    Console.Write( "Введите количество товара: " );
    while ( !int.TryParse( Console.ReadLine(), out quantity ) || quantity <= 0 )
    {
        Console.WriteLine( "Ошибка: введите корректное целое число больше 0" );
        Console.Write( "Введите количество товара: " );
    }

    return quantity;
}

void PrintHeader()
{
    const string header = "=== Оформление заказа ===\n";
    Console.WriteLine( header );
}

string ReadNonEmptyLine( string prompt )
{
    Console.Write( prompt );
    string input = "";
    while ( string.IsNullOrEmpty( input ) )
    {
        input = Console.ReadLine()?.Trim() ?? string.Empty;
        if ( string.IsNullOrWhiteSpace( input ) )
        {
            Console.Write( "Ввод не может быть пустым. Попробуйте снова: " );
        }
    }

    return input;
}

bool ConfirmOrder( string name, string product, int quantity, string address )
{
    HashSet<string> successAnswers = [ "да", "y", "yes" ];
    HashSet<string> cancelAnswers = [ "нет", "no", "n" ];

    while ( true )
    {
        string response = ReadNonEmptyLine(
            $"\nЗдравствуйте, {name}, вы заказали {quantity} {product} на адрес {address}, все верно? (да/нет): "
        );

        if ( successAnswers.Contains( response ) )
        {
            return true;
        }

        if ( cancelAnswers.Contains( response ) )
        {
            return false;
        }

        Console.WriteLine( "Ошибка: введите 'да', 'y', 'yes', 'нет', 'n', 'no'" );
    }
}

void ShowSuccessMessage(
    string name,
    string product,
    int quantity,
    string address,
    DateTime deliveryDate
)
{
    Console.WriteLine(
        $"\n{name}! Ваш заказ {product} в количестве {quantity} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate:dd.MM.yyyy}" );
}

void ShowCancelMessage( string name, string product )
{
    Console.WriteLine( $"\n{name}! Ваш заказ {product} отменен" );
}
