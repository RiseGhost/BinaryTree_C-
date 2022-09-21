using create_tree;
using style_manager;
using System.Text;
Console.OutputEncoding = Encoding.UTF8; //Read emojis.
style style = new style();

//Retorna uma árvore com tamanho e elemento defenidos pelo utilizador:
tree Create_tree(int element_number){
    tree acacia = new tree();
    for (int i = 0; i < element_number; i++){
        Console.Write("--> ");
        string? number = Console.ReadLine();
        if(number != null)  acacia.add(Int32.Parse(number));
    }
    return acacia;
}

//Responsável por gerar uma árvore com números aleatórios:
//element_number -> número de elementos da árvore binária
//max -> número aleatório máximo
//min -> número alaetório minimo
tree Generate_Tree(int element_number, int max, int min){
    Random rd = new Random();
    tree t = new tree();
    for(int i = 0; i < element_number; i++){
        int random = rd.Next(min, max);
        while (t.search(random) == true){
            random = rd.Next(min, max);
        }
        t.add(random);
    }
    return t;
}

//Retorna True se existir um númera na columa colmn e False caso contrário:
bool ColumnCheckNumber(string[,] str, int column){
    int row = str.GetLength(0);
    for(int r = 0; r < row; r++){
        if (str[r, column] != null) return true;
    }
    return false;
}

//Adiciona formatação ao array str, mas concretamente espaços em branco:
string[,] AddFormat(string[,] str, tree t){
    int row = str.GetLength(0);
    int column = str.GetLength(1);
    int max = t.maxvalue().ToString().Count();
    string s = "";
    while (s.Count() < max){
        s += " ";
    }
    for(int r = 0; r < row; r++){
        for(int c = 0; c < column; c++){
            if (str[r, c] == null && ColumnCheckNumber(str, c)) str[r,c] = s;
        }
    }
    return str;
}

//Imprime o array str:
void print_table(string[,] str, tree t){
    Console.Clear();
    int row = str.GetLength(0);
    int column = str.GetLength(1);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("alturas/niveis:");
    for(int r = 0; r < row; r++){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(r + " -> ");
        if(r != 0) Console.ForegroundColor = ConsoleColor.Yellow;
        else Console.ForegroundColor = ConsoleColor.Green;
        for(int c = 0; c < column; c++){
            if(c == (column/2) + 1) Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(str[r, c]);
        }
        Console.WriteLine();
    }
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Parte direita da árvore representada em Ciano.");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Parte esquedra da árvore representada em amarelo.");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Maior valor = " + t.maxvalue() + " Menor valor = " + t.minvalue());
    Console.WriteLine("Número de elementos = " + t.Count());
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Lista com todos os elementos da árvore binária:");
    style.print_tree_value(t);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("Clique R");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" para carregar uma nova árvore binária com o mesmo intervalor de números");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("Clique noutra tecla");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" qualquer para sair");
}

//Verefica se existe algum problema com os inputs.
//Se existir imprime uma mensagem de erro:
bool err_msg(string? str){
    if (str == null || str == "" || str == " "){
        Console.Clear();
        Console.WriteLine("❌ Erro ❌" + "\n" + "Valor inválido");
        return false;
    }
    else return true;
}

void menu2(){
    Console.Write("Indique o número de elementos da árvore binária: ");
    string? number = Console.ReadLine();
    Console.Clear();
    int element_number = 0;
    if (number != null && number != "") {
        element_number = Int16.Parse(number);
        tree acacia = Create_tree(element_number);
        print_table(AddFormat(acacia.ToTable(), acacia), acacia);
    }   else err_msg(number);
}

void menu1(){
    int element_number, max, min;
    string?[] input = new string?[3];
    Console.Write("Número de elementos -> ");
    input[0] = Console.ReadLine();
    Console.Write("Valor Máximo -> ");
    input[1] = Console.ReadLine();
    Console.Write("Valor Minimo -> ");
    input[2] = Console.ReadLine();
    if(err_msg(input[0]) && err_msg(input[1]) && err_msg(input[2])){
        element_number = Int16.Parse(input[0]);
        max = Int16.Parse(input[1]);
        min = Int16.Parse(input[2]);
        if (min > max){
            Console.Clear();
            Console.WriteLine("❌ Erro ❌" + "\n" + "O máximo não pode ser menor que o minimo.");
        }   else{
            do{
                tree acacia = Generate_Tree(element_number, max, min);
                print_table(AddFormat(acacia.ToTable(), acacia), acacia);
            }while(Console.ReadKey().Key == ConsoleKey.R);
        }
    }
}

void main(){
    int item_select = 1;
    style.menu(item_select);
    var Key = Console.ReadKey().Key;
    while(Key != ConsoleKey.Enter){
        if(Key == ConsoleKey.UpArrow){
            item_select--;
        }   else if (Key == ConsoleKey.DownArrow){
            item_select++;
        }
        style.menu(item_select);
        Key = Console.ReadKey().Key;
    }
    Console.Clear();
    if (item_select % 2 == 0) menu2();
    else menu1();
}


main();
