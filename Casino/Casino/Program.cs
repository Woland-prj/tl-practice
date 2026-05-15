using Casino;

const int minRandomNumber = 1;
const int maxRandomNumber = 20;
const int winFromNumber = 18;
const int multiplicator = 25;
const int simpleDivider = 17;

double balance = 0;
bool isGameExit = false;

PrintHeader();
SetInitialBalance();

while ( !isGameExit )
{
    PrintMenu();
    string option = Console.ReadLine()?.Trim() ?? string.Empty;
    OptionHandleResult result = HandleOption( option );
    
    if ( result == OptionHandleResult.Exit )
    {
        isGameExit = true;
        continue;
    }
    
    if ( result != OptionHandleResult.Success )
    {
        PrintErrorMessage( result );
    }
}

void PrintHeader()
{
    Console.WriteLine( "####################" );
    Console.WriteLine( "####   CASINO   ####" );
    Console.WriteLine( "####################" );
}

void SetInitialBalance()
{
    Console.Write( "Введите начальный баланс: " );

    double initialBalance;

    while ( !double.TryParse( Console.ReadLine(), out initialBalance ) || initialBalance <= 0 )
    {
        Console.Write( "Введите корректный баланс: " );
    }

    IncreaseBalance( initialBalance );
}

void PrintMenu()
{
    List<string> menuOptions =
    [
        "1. Пополнить балланс",
        "2. Показать баланс",
        "3. Сыграть",
        "4. Выйти"
    ];

    foreach ( string option in menuOptions )
    {
        Console.WriteLine( option );
    }
}

OptionHandleResult HandleOption( string option )
{
    return option switch
    {
        "1" => MakeDeposit(),
        "2" => ShowBalance(),
        "3" => Play(),
        "4" => Exit(),
        _ => OptionHandleResult.InvalidOption,
    };
}

OptionHandleResult MakeDeposit()
{
    Console.Write( "Введите депозит: " );

    string depositStr = Console.ReadLine()?.Trim() ?? string.Empty;

    if ( !double.TryParse( depositStr, out double deposit ) || deposit <= 0 )
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    OptionHandleResult result = IncreaseBalance( deposit );

    if ( result != OptionHandleResult.Success )
    {
        return result;
    }

    Console.WriteLine( $"Баланс успешно пополнен. Текущий баланс: {balance:F2}" );

    return OptionHandleResult.Success;
}

OptionHandleResult ShowBalance()
{
    Console.WriteLine( $"Текущий баланс: {balance}" );

    return OptionHandleResult.Success;
}

OptionHandleResult Play()
{
    Console.Write( "Введите ставку: " );

    if ( !double.TryParse( Console.ReadLine(), out double bet ) || bet <= 0 )
    {
        return OptionHandleResult.InvalidBet;
    }

    if ( bet > balance )
    {
        return OptionHandleResult.NotEnoughBalance;
    }

    int randomNumber = Random.Shared.Next( minRandomNumber, maxRandomNumber + 1 );

    if ( randomNumber >= winFromNumber )
    {
        double winAmount = CalculateWinAmount( bet, randomNumber );
        balance += winAmount;
        Console.WriteLine( $"Вы выиграли {winAmount:F2}" );
    }
    else
    {
        balance -= bet;
        Console.WriteLine( $"Вы проиграли {bet:F2}" );
    }

    return OptionHandleResult.Success;
}

double CalculateWinAmount( double bet, int seed )
{
    Console.WriteLine( $"seed: {seed}" );
    int winPercent = multiplicator * ( seed % simpleDivider );
    Console.WriteLine( $"win percent: {winPercent}" );

    return bet * ( 1 + winPercent / 100.0 );
}

OptionHandleResult Exit()
{
    Console.WriteLine( "Выход из игры..." );

    return OptionHandleResult.Exit;
}

OptionHandleResult IncreaseBalance( double deposit )
{
    if ( double.MaxValue - deposit < balance )
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    balance += deposit;

    return OptionHandleResult.Success;
}

void PrintErrorMessage( OptionHandleResult result )
{
    string message = result switch
    {
        OptionHandleResult.InvalidOption => "Неверный пункт меню",
        OptionHandleResult.InvalidDepositValue => "Некорректная сумма депозита",
        OptionHandleResult.InvalidBet => "Некорректная ставка",
        OptionHandleResult.NotEnoughBalance => "Недостаточно средств",
        _ => "Произошла ошибка"
    };

    Console.WriteLine( message );
}