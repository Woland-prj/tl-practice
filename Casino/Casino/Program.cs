const int minRandomNumber = 1;
const int maxRandomNumber = 20;
const int winFromNumber = 18;
const int multiplicator = 25;
const int simpleDivider = 17;

double ballance = 0;
bool isGameExit = false;

PrintHeader();
SetInitialBalance();

while ( !isGameExit )
{
    PrintMenu();
    string option = Console.ReadLine()?.Trim() ?? string.Empty;
    OptionHandleResult res = HandleOption( option );
    if ( res != OptionHandleResult.Success )
    {
        PrintErrorMessage( res );
    }
}

void SetInitialBalance()
{
    Console.Write( "Введите начальный баланс: " );

    double initialBalance;

    while ( !double.TryParse( Console.ReadLine(), out initialBalance ) || initialBalance <= 0 )
    {
        Console.Write( "Введите корректный баланс: " );
    }

    IncreaseBallance( initialBalance );
}

OptionHandleResult HandleOption( string opt )
{
    return opt switch
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
    Console.WriteLine( "Введите депозит: " );

    string depositStr = Console.ReadLine()?.Trim() ?? string.Empty;

    if ( !double.TryParse( depositStr, out double deposit ) || deposit <= 0 )
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    OptionHandleResult res = IncreaseBallance( deposit );

    if ( res != OptionHandleResult.Success )
    {
        return res;
    }

    Console.WriteLine( $"Баланс успешно пополнен. Текущий баланс: {ballance:F2}" );

    return OptionHandleResult.Success;
}

OptionHandleResult IncreaseBallance( double deposit )
{
    if ( double.MaxValue - deposit < ballance )
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    ballance += deposit;

    return OptionHandleResult.Success;
}

OptionHandleResult ShowBalance()
{
    Console.WriteLine( $"Текущий баланс: {ballance}" );

    return OptionHandleResult.Success;
}

OptionHandleResult Play()
{
    Console.Write( "Введите ставку: " );

    if ( !double.TryParse( Console.ReadLine(), out double bet ) || bet <= 0 )
    {
        return OptionHandleResult.InvalidBet;
    }

    if ( bet > ballance )
    {
        return OptionHandleResult.NotEnoughBalance;
    }

    int randomNumber = Random.Shared.Next( minRandomNumber, maxRandomNumber + 1 );

    if ( randomNumber >= winFromNumber )
    {
        double winAmount = CalculateWinAmount( bet, randomNumber );
        ballance += winAmount;
        Console.WriteLine( $"Вы выиграли {winAmount:F2}" );
    }
    else
    {
        ballance -= bet;
        Console.WriteLine( $"Вы проиграли {bet:F2}" );
    }

    return OptionHandleResult.Success;
}

double CalculateWinAmount( double bet, int seed )
{
    Console.WriteLine( $"seed: {seed}" );
    int winPercent = multiplicator * ( seed % simpleDivider );
    Console.WriteLine( $"win percent: {winPercent}" );
    if ( winPercent < 0 )
        return 0;

    return bet * ( winPercent / 100.0 );
}

OptionHandleResult Exit()
{
    isGameExit = true;
    Console.WriteLine( "Выход из игры..." );

    return OptionHandleResult.Success;
}

void PrintHeader()

{
    Console.WriteLine( "####################" );
    Console.WriteLine( "####   CASINO   ####" );
    Console.WriteLine( "####################" );
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

    foreach ( var option in menuOptions )
    {
        Console.WriteLine( option );
    }
}

void PrintErrorMessage( OptionHandleResult result )
{
    string message = result switch
    {
        OptionHandleResult.InvalidOption =>
            "Неверный пункт меню",

        OptionHandleResult.InvalidDepositValue =>
            "Некорректная сумма депозита",

        OptionHandleResult.InvalidBet =>
            "Некорректная ставка",

        OptionHandleResult.NotEnoughBalance =>
            "Недостаточно средств",

        _ =>
            "Произошла ошибка"
    };

    Console.WriteLine( message );
}

enum OptionHandleResult
{
    Success = 0,
    InvalidOption = 1,

    InvalidDepositValue = 2,

    InvalidBet = 3,

    NotEnoughBalance = 4,
}
