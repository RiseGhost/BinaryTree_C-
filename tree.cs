namespace create_tree{
    public class tree
    {
        // ? -> server para que o valor possa tomar o valor de null:
        public int? data;
        public tree? rigth, left = null;
        private tree_func func = new tree_func();
    
        public tree(){
            this.data = null;
        }

        public tree(int data){
            this.data = data;
        }

        //Responsável por adicionar valores a árvore binária:
        public void add(int number){
            if(data == null){
                data = number;
            }   else if (data > number){
                if (left != null) left.add(number);
                else left = new tree(number);
            }   else if (data < number){
                if (rigth != null) rigth.add(number);
                else rigth = new tree(number);
            }
        }

        //Imprimir a árvore binária da esquedra para a direita:
        public void print(){
            if (left != null) left.print();
            if (data != null) Console.WriteLine(data);
            if (rigth != null) rigth.print();
        }

        //Return the total number of elements:
        public int Count(){
            if (data != null){
                if (rigth != null && left != null) return 1 + rigth.Count() + left.Count();
                else if (rigth != null) return 1 + rigth.Count();
                else if (left != null) return 1 + left.Count();
                else return 1;
            }
            else return 0;
        }

        //Return the tree element on List:
        //***************************************
        private void ConvertToList(List<int> l){
            if (data == null) return;
            l.Add((int)data);
            if (left != null) left.ConvertToList(l);
            if (rigth != null) rigth.ConvertToList(l);
        }

        public List<int> ToList(){
            List<int> result = new List<int>();
            this.ConvertToList(result);
            return result;
        }
        //***************************************

        //Retorna True se o número existir na árvore binária e False caso o número não exista na árvore binária:
        public bool search(int number){
            return func.search(this, number);
        }

        //Retorna a altura da árvore binária:
        public int height(){
            return func.height(this);
        }

        //Retorna uma lista com todos os elementos daquela altura:
        public List<int> height_list(int height){
            List<int> l = new List<int>();
            func.height_list(this, height, l);
            return l;
        }

        public int maxvalue(){
            return func.maxvalue(this);
        }

        public int minvalue(){
            return func.minvalue(this);
        }

        //Imprime a árvore binária por altura:
        public void print_by_line(){
            for(int j = 0; j <= this.height(); j++){
                List<int> l = this.height_list(j);
                for(int i = l.Count() - 1; i >= 0; i--){
                    Console.Write(l[i] + " ");
                }
                Console.WriteLine();
            }
        }

        //Retorna o Pai de uma valor de valor da árvore binária:
        public int? father_number(int value){
            if (rigth != null && rigth.data == value) return data;
            else if(left != null && left.data == value) return data;
            if (rigth != null && left != null) return func.SomaNull(rigth.father_number(value), left.father_number(value));
            else if (rigth == null && left != null) return func.SomaNull(null, left.father_number(value));
            else if (rigth != null && left == null) return func.SomaNull(rigth.father_number(value), null);
            else return null;
        }

        //Retorna o número de casas decimais antes a virgula de um número maior que 1:
        private int DecimalPlace(double number){
            if (number < 1) return 0;
            else return 1 + DecimalPlace(number / 10);
        }

        //Retorna 1 se o número aredondar para o número seguinte ou 0 se não aredondar:
        private int around(double number){
            if (number == 0 || number < 0.5) return 0;
            else if (number >= 1) return around(number - func.expoenete(10, this.DecimalPlace(number) - 1));
            else return 1;
        }

        //Transforma a Árvore Binária numa tabela:
        public string[,] ToTable(){
            int altura = this.height() + 1;
            int column = 2 * func.expoenete(2, altura) - 1;
            string[,] table = new string[altura,column];
            table[0, column/2 + around((double)column/2) - 1] = func.NullableInt_to_String(data);
            for (int i = 1; i < altura; i++){
                List<int> line = this.height_list(i); //lista que contém os elementos da lina/altura i da árvore.
                int delta = (column/(func.expoenete(2, i + 1))) + this.around((double) (column/(2 * func.expoenete(2, i))));
                for (int j = 0; j < line.Count(); j++){
                    int? father = this.father_number(line[j]);
                    int father_column = func.SearchFatherTable(table, i - 1,func.NullableInt_to_String(father));
                    if (father > line[j]){
                        int new_column = father_column - delta;
                        table[i, new_column] = line[j].ToString();
                    }   else{
                        int new_column = father_column + delta;
                        table[i, new_column] = line[j].ToString();
                    }
                }
            }
            return table;
        }
    }

    //Classe que contém funções para manipular árvores binárias:
    public class tree_func{
        
        public int expoenete(int b, int e){
            if ( e == 0 ) return 1;
            else return b * expoenete(b, e - 1);
        }
        
        public bool search(tree t, int element){
            if (t == null || t.data == null) return false;
            else{
                if (t.data == element) return true;
                else return false || search(t.rigth, element) || search(t.left, element);
            }
        }

        //Converte um nullable int em um int normal:
        private int NullableInt_to_Int(int? number){
            if (number == null) return default(int);
            else return number.Value;
        }

        //Converte um nullable int em uma string:
        public string NullableInt_to_String(int? number){
            if (number == null) return " ";
            else return number.Value.ToString();
        }

        //Retorna o maior valor da árvore binária:
        public int maxvalue(tree t){
            if(t.rigth != null) return maxvalue(t.rigth);
            else return NullableInt_to_Int(t.data);
        }

        //Retorna o manor valor da árvore binária:
        public int minvalue(tree t){
            if(t.left != null) return minvalue(t.left);
            else return NullableInt_to_Int(t.data);
        }

        //Retorna o máximo entre dois números:
        private int max(int x, int y){
            if (x > y) return x;
            else return y;
        }

        //Faz a soma entre dois Nullable Int.
        //Se forem os dois null retorna null, se um for null e outro não retorna aquele que não é null.
        //Se os dois não forem null retorna a soma normal:
        public int? SomaNull(int? x, int? y){
            if (x == null && y == null)  return null;
            else if (x == null && y != null) return y;
            else if (x != null && y == null) return x;
            else return x + y;
        }

        public int height(tree t){
            if (t != null && t.data != null) return 1 + max(height(t.rigth), height(t.left));
            else return -1;
        }

        //Adiciona todos os elmenetos que estão a altura height a lista:
        public void height_list(tree t, int height, List<int> l){
            if (t != null && t.data != null){
                if (height == 0) l.Add(NullableInt_to_Int(t.data));
                height_list(t.rigth, height - 1, l);
                height_list(t.left, height - 1, l);
            }
        }

        //Retorna a columa onde esta o pai do elemento.
        //Se o elemento não existir naquela linha retorna -1:
        public int SearchFatherTable(string[,] str, int heigth, string value){
            int column = str.GetLength(1);
            for (int i = 0; i < column; i++){
                if(str[heigth, i] == value) return i;
            }
            return -1;
        }
    }
}