using create_tree;

namespace style_manager{
    public class style{
        int top_margin;

        public style(){
            top_margin = 0;
        }

        public void AlignText(string str, string Position){
            if (Position == "left") {
                Console.SetCursorPosition(Console.WindowWidth - str.Count(), top_margin);
            }   else if (Position == "center"){
                Console.SetCursorPosition((Console.WindowWidth - str.Count())/2, top_margin);
            }   else{
                Console.SetCursorPosition(0, top_margin);
            }
            Console.WriteLine(str);
            top_margin++;
        }

        public void menu(int item){
            top_margin = 0;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            AlignText("**** 🌳 C# Binary Tree System 🌳 ****", "center");
            Console.ForegroundColor = ConsoleColor.White;
            if (item % 2 != 0){
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                AlignText("➡️  1 -> Gerar árvore 🌳 com valores aleatórios 🎲.", "center");
                Console.ForegroundColor = ConsoleColor.White;
                AlignText("2 -> Gerar árvore 🌳 com valores escolhidos pelo usuário.", "center");
            }   else{
                AlignText("1 -> Gerar árvore 🌳 com valores aleatórios 🎲.", "center");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                AlignText("➡️  2 -> Gerar árvore 🌳 com valores escolhidos pelo usuário.", "center");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("Use ↑ ↓ to navigate the menu.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("PRESS ENTER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" to select.");
        }

        public void print_tree_value(tree acacia){
            List<int> tree_list = acacia.ToList();
            Console.Write("[");
            for(int i = 0; i < tree_list.Count() - 1; i++){
                Console.Write(tree_list[i] + " ,");
            }
            Console.WriteLine(tree_list[tree_list.Count() - 1] + "]");
        }
    }
}