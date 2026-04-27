PrintHeader();

string product = GetProductName();
int quantity = GetQuantity();
string name = GetUserName();
string address = GetAddress();

bool isConfirmed = ConfirmOrder( name, product, quantity, address );

if ( isConfirmed )
{
    DateTime deliveryDate = DateTime.Today.AddDays( 3 );
    ShowSuccessMessage( name, product, quantity, address, deliveryDate );
}
else
{
    ShowCancelMessage( name, product );
}

Console.WriteLine( "\nНажмите любую клавишу для выхода..." );
Console.ReadKey();

string GetProductName()
{
    Console.Write( "Введите название товара: " );
    return ReadNonEmptyLine();
}

int GetQuantity()
{
    int quantity = 0;
    Console.Write( "Введите количество товара: " );
    while ( !int.TryParse( Console.ReadLine(), out quantity ) || quantity <= 0 )
    {
        Console.WriteLine( "Ошибка: введите корректное целое число больше 0" );
        Console.Write( "Введите количество товара: " );
    }

    return quantity;
}

string GetUserName()
{
    Console.Write( "Введите ваше имя: " );
    return ReadNonEmptyLine();
}

string GetAddress()
{
    Console.Write( "Введите адрес доставки: " );
    return ReadNonEmptyLine();
}

void PrintHeader()
{
    const string header = "=== Оформление заказа ===\n";
    Console.WriteLine( header );
}

string ReadNonEmptyLine()
{
    string input;
    do
    {
        input = Console.ReadLine()?.Trim();
        if ( string.IsNullOrWhiteSpace( input ) )
        {
            Console.Write( "Поле не может быть пустым. Попробуйте снова:" );
        }
    } while ( string.IsNullOrWhiteSpace( input ) );

    return input;
}

bool ConfirmOrder( string name, string product, int quantity, string address )
{
    HashSet<string> successMsgs = [ "да", "y", "yes" ];
    Console.WriteLine(
        $"\nЗдравствуйте, {name}, вы заказали {quantity} {product} на адрес {address}, все верно? (да/нет)" );
    string response = Console.ReadLine()?.Trim().ToLowerInvariant();
    return successMsgs.Contains( response );
}

void ShowSuccessMessage( string name, string product, int quantity, string address, DateTime deliveryDate )
{
    Console.WriteLine(
        $"\n{name}! Ваш заказ {product} в количестве {quantity} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate:dd.MM.yyyy}" );
}

void ShowCancelMessage( string name, string product )
{
    Console.WriteLine( $"\n{name}! Ваш заказ {product} отменен" );
}