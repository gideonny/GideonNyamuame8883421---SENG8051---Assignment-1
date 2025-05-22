Console.WriteLine("Hello! Welcome to Virtual Pet!");

string petType = "";
bool isValid = false;

// Loop until a valid pet type is selected
while (!isValid)
{
    Console.WriteLine("Please select a pet by typing in the corresponding number.");
    Console.WriteLine("1. Dog");
    Console.WriteLine("2. Cat");
    Console.WriteLine("3. Fish" + Environment.NewLine);
    petType = Console.ReadLine();

    if (petType == "1")
    {
        petType = "Dog";
        Console.WriteLine(Environment.NewLine + "You have selected a dog!");
        isValid = true;
    }
    else if (petType == "2")
    {
        petType = "Cat";
        Console.WriteLine(Environment.NewLine + "You have selected a cat!");
        isValid = true;
    }
    else if (petType == "3")
    {
        petType = "Fish";
        Console.WriteLine(Environment.NewLine + "You have selected a fish!");
        isValid = true;
    }
    else
    {
        Console.WriteLine(Environment.NewLine + "Invalid selection. Please try again.");
    }
}

Console.WriteLine(Environment.NewLine + $"What would you like to name your {petType.ToLower()}?");
string petName = Console.ReadLine();

Console.WriteLine(Environment.NewLine + $"Enjoy! Please take good care of {petName} the {petType.ToLower()}!" + Environment.NewLine);

Console.WriteLine($"Interact with {petName}, your pet {petType.ToLower()}, by pressing the number that corresponds to the menu option you want. Remember to check {petName}'s status often to ensure you are taking good care of them! Every action affects {petName}'s hunger, health, and happiness. Make sure they don't get too hungry or sad, and most importantly, keep them in good health. Enjoy!" + Environment.NewLine);

DateTime timeOfDay = DateTime.Today.AddHours(8); // Start at 8 AM
bool skipTimeUpdate = false; // to skip time update for 2 hour rest

// Pet stats
int hunger = 5;
int happiness = 5;
int health = 5;

Console.WriteLine("Please select an option by typing in the corresponding number from the menu below." + Environment.NewLine);
Console.WriteLine($"The time is: {timeOfDay:hh:mm tt}" + Environment.NewLine);

// Display the menu
Console.WriteLine("Menu:");
Console.WriteLine($"1. Feed {petName}");
Console.WriteLine($"2. Play with {petName}");
Console.WriteLine($"3. Let {petName} rest");
Console.WriteLine($"4. Do nothing");
Console.WriteLine($"5. Check {petName}'s status");
Console.WriteLine("6. Exit" + Environment.NewLine);

string menuChoice = Console.ReadLine();

// Main loop
while (menuChoice != "6")
{
    switch (menuChoice)
    {
        case "1":
            Console.WriteLine($"You fed {petName}.");

            hunger -= 3;
            happiness += 2;

            if (hunger <= 1)
            {
                Console.WriteLine($"{petName} ate too much and has an upset tummy!");
                health -= 1;
            }
            else
            {
                health += 3;
            }
            break;

        case "2":
            if (hunger >= 8)
            {
                Console.WriteLine($"{petName} is too hungry to play!");
                break;
            }
            else
            {
                Console.WriteLine($"You played with {petName}.");
                hunger += 1;
                happiness += 4;
                health += 1;
            }
            break;

        case "3":
            Console.WriteLine($"{petName} is resting for two hours.");
            timeOfDay = timeOfDay.AddHours(2);  // Advance 2 hours for rest
            hunger += 2;       
            happiness -= 2;    
            health += 6;      
            skipTimeUpdate = true;  // <-- SET FLAG HERE
            break;

        case "4":
            Console.WriteLine($"You chose to do nothing. {petName} had an uneventful hour.");
            break;

        case "5":
            Console.WriteLine($"{petName}'s status:");
            Console.WriteLine($"Hunger: {hunger}");
            Console.WriteLine($"Happiness: {happiness}");
            Console.WriteLine($"Health: {health}");
            break;

        default:
            Console.WriteLine("Invalid selection. Please try again.");
            break;
    }

    // Cap stats between 0 and 10
    if (hunger < 0) hunger = 0;
    if (hunger > 10) hunger = 10;

    if (happiness < 0) happiness = 0;
    if (happiness > 10) happiness = 10;

    if (health < 0) health = 0;
    if (health > 10) health = 10;

    // Check if pet is very hungry or too full
    if (hunger >= 8)
        Console.WriteLine($"{petName} is getting very hungry!");
    else if (hunger <= 2)
        Console.WriteLine($"{petName} is too full and may feel sick.");

    // Health deterioration due to neglect
    if (hunger >= 9 || happiness <= 2)
    {
        health -= 1;
        if (health < 0) health = 0;
        Console.WriteLine($"{petName}'s health is deteriorating due to neglect!");
    }

    // Health/happiness warnings
    if (health <= 2)
        Console.WriteLine($"{petName} is in poor health!");

    if (happiness <= 3)
        Console.WriteLine($"{petName} is feeling very sad.");

    // Advance time
    if (!skipTimeUpdate)
    {
        timeOfDay = timeOfDay.AddHours(1);
        hunger += 1;
        happiness -= 1;
    }

    // Reset the flag for next iteration
    skipTimeUpdate = false; 

    // Cap again after time update
    if (hunger > 10) hunger = 10;
    if (happiness < 0) happiness = 0;

    // Collapse check
    if (health <= 0 || happiness <= 0 || hunger >= 10)
    {
        Console.WriteLine($"\n{petName} has collapsed due to poor condition.");
        Console.WriteLine("Would you like to rush them to the vet and continue? (y/n)");

        string choice = Console.ReadLine().ToLower();
        if (choice == "y")
        {
            Console.WriteLine($"{petName} is being cared for...");
            hunger = 5;
            happiness = 3;
            health = 3;
        }
        else
        {
            Console.WriteLine($"You chose to let {petName} rest. Thank you for the time you spent together.");
            break;
        }
    }


    Console.WriteLine(Environment.NewLine + $"Time is now: {timeOfDay:hh:mm tt}");

    // Show menu again
    Console.WriteLine("Menu:");
    Console.WriteLine($"1. Feed {petName}");
    Console.WriteLine($"2. Play with {petName}");
    Console.WriteLine($"3. Let {petName} rest");
    Console.WriteLine($"4. Do nothing");
    Console.WriteLine($"5. Check {petName}'s status");
    Console.WriteLine("6. Exit" + Environment.NewLine);

    menuChoice = Console.ReadLine();
}

Console.WriteLine($"Thanks for playing! Here are {petName}'s final Stats - Hunger: {hunger}, Happiness: {happiness}, Health: {health}.");
Console.WriteLine($"Goodbye! {petName} will miss you!");
