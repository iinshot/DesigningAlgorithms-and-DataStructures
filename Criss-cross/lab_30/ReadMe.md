**Задача:** создать алгоритм, которой будет генерировать таблицу пересекающихся по вертикали и горизонтали слов, также необходима наибольшая связность. 

Опишем методы и поля, которые позволят нам выполнить эту задачу, а также оптимизировать отрисовку крисс-кросса.

**Начнем с описания полей:**
```
private List<string> words = new List<string>();
private const int cellSize = 30;
private const int borderSize = 1;
private Dictionary<string, (int x, int y, bool isHorizontal)> placement = new Dictionary<string, (int x, int y, bool isHorizontal)>();
private char[,] grid;
private int gridWidth = 0;
private int gridHeight = 0;
```
1) words: список для хранения слов, которые ввел пользователь
2) cellSize: константа, которая определяет размер ячейки сетки
3) borderSize: константа, которая определяет размер границы ячейки
4) placement: словарь, в котором хранится информация о размещении слов на сетке (координаты и ориентация)
5) grid: двумерный массив букв, который представляет собой сетку
6) gridWidth, gridHeight: размеры сетки

**Далее по очереди опишем методы и принципы их работы:**
1) DrawPuzzle
Данный метод отрисовывает сетку крисс-кросс на Canvas (Canvas используется для точного позиционирования элементов на экране). Для каждого слова в словаре он создает ячейку и текстовый блок и размещает их в правильных координатах. Этот метод обрабатывает вертикальные и горизонтальные слова. Также метод регулирует отступы при отрисовке.
*Приведем реализацию данного метода:*
```
private void DrawPuzzle()
{
    if (placement == null || placement.Count == 0) return;
    double canvasWidth = gridWidth * cellSize + 20;
    double canvasHeight = gridHeight * cellSize + 20;
    gridCanvas.Width = canvasWidth;
    gridCanvas.Height = canvasHeight;
    foreach (var wordPlacement in placement)
    {
        string word = wordPlacement.Key;
        (int x, int y, bool isHorizontal) pos = wordPlacement.Value;
        for (int i = 0; i < word.Length; i++)
        {
            int x = pos.x;
            int y = pos.y;
            if (pos.isHorizontal)
                x += i;
            else
                y += i;
            var rect = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Stroke = Brushes.Black,
                StrokeThickness = borderSize,
                Fill = Brushes.White,
            };
            Canvas.SetLeft(rect, x * cellSize - 10);
            Canvas.SetTop(rect, y * cellSize - 10);
            var textBlock = new TextBlock
            {
                Text = word[i].ToString(),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = cellSize * 0.7,
                FontWeight = FontWeights.Bold,
                Width = cellSize,
                Height = cellSize
            };
            Canvas.SetLeft(textBlock, x * cellSize - 10);
            Canvas.SetTop(textBlock, y * cellSize - 10);
            gridCanvas.Children.Add(rect);
            gridCanvas.Children.Add(textBlock);
        }
    }
}
```

2) TryPlaceWordAt
Данный метод осуществляет проверку: можно ли разместить в данной позиции и ориентации, не накладываясь на другие слова. Если это возможно, то метод вернет координаты, количество пересечений и ориентацию. В противном же случае вернет null.
*Приведем реализацию данного метода:*
```
private (int x, int y, int crossCount, bool isHorizontal)? TryPlaceWordAt(string word, int x, int y, bool isHorizontal)
{
    int crossCount = 0;
    if (x < 0 || y < 0 || x + (isHorizontal ? word.Length : 0) >= gridWidth || y + (!isHorizontal ? word.Length : 0) >= gridHeight)
    {
        return null;
    }
    for (int i = 0; i < word.Length; i++)
    {
        int gridX = x;
        int gridY = y;
        if (isHorizontal)
            gridX += i;
        else
            gridY += i;
        if (grid[gridY, gridX] != '\0')
        {
            if (placement.Any(p =>
            {
                var (px, py, pIsHorizontal) = p.Value;

                if (pIsHorizontal)
                {
                    if (gridY == py && gridX >= px && gridX < px + p.Key.Length)
                        return true;
                }
                else
                {
                    if (gridX == px && gridY >= py && gridY < py + p.Key.Length)
                        return true;
                }
                return false;
            }))
            {
                if (placement.Any(p =>
                {
                    var (px, py, pIsHorizontal) = p.Value;
                    if (pIsHorizontal)
                        if (gridY == py && gridX >= px && gridX < px + p.Key.Length)
                            return isHorizontal;
                    else
                        if (gridX == px && gridY >= py && gridY < py + p.Key.Length)
                            return !isHorizontal;
                    return false;
                }))
                {
                    return null;
                }
                crossCount++;
            }
        }
    }
    for (int i = 0; i < word.Length; i++)
    {
        int gridX = x;
        int gridY = y;
        if (isHorizontal)
            gridX += i;
        else
            gridY += i;
        if (grid[gridY, gridX] != '\0' && grid[gridY, gridX] != word[i])
            return null;
    }
    return (x, y, crossCount, isHorizontal);
}
```

3) FindBestPlacement
Данный метод перебирает всевозможные позиции и ориентации для нового слова, проверяя пересечения со старыми словами. Метод ищет ячейку, в которой символ слова совпадает с символом в сетке, а затем пытается разместить слово горизонтально или вертикально. Метод возвращает лучшие координаты и ориентацию, при которых получается больше всего пересечений, в противном случае возвращает null. Этот метод использует метод TryPlaceWordAt для того, чтобы оценить каждое место. 
*Приведем реализацию данного метода:*
```
private (int x, int y, bool isHorizontal)? FindBestPlacement(string word)
{
    int bestCrossCount = -1;
    (int x, int y, bool isHorizontal)? bestPlacement = null;

    for (int row = 0; row < gridHeight; row++)
        for (int col = 0; col < gridWidth; col++)
        {
            if (grid[row, col] == '\0')
                continue;

            for (int charIndex = 0; charIndex < word.Length; charIndex++)
                if (grid[row, col] == word[charIndex])
                {
                    var ver = TryPlaceWordAt(word, col, row - charIndex, false);
                    if (ver.HasValue)
                    {
                        int crossCount = ver.Value.crossCount;
                        if (crossCount > bestCrossCount)
                        {
                            bestCrossCount = crossCount;
                            bestPlacement = (ver.Value.x, ver.Value.y, ver.Value.isHorizontal);
                        }
                    }
                    var hor = TryPlaceWordAt(word, col - charIndex, row, true);
                    if (hor.HasValue)
                    {
                        int crossCount = hor.Value.crossCount;
                        if (crossCount > bestCrossCount)
                        {
                            bestCrossCount = crossCount;
                            bestPlacement = (hor.Value.x, hor.Value.y, hor.Value.isHorizontal);
                        }
                    }

                }
        }
    return bestPlacement;
}
```

4) PlaceWord
Данный метод находит лучшее место для слова, вызывая метод FindBestPlacement. В случае, если такое место найдено, то метод обновляет сетку и добавляет новое слово в соответствии с координатами и ориентацией. В конце он обновляет размеры сетки и заменяет старую сетку на новую с уже добавленным словом.
*Приведем реализацию данного метода:*
```
private void PlaceWord(string word)
{
    (int x, int y, bool isHorizontal)? bestPlacement = FindBestPlacement(word);
    if (bestPlacement.HasValue)
    {
        var placementInfo = bestPlacement.Value;
        var x = placementInfo.x;
        var y = placementInfo.y;
        var isHorizontal = placementInfo.isHorizontal;
        for (int i = 0; i < word.Length; i++)
        {
            if (!isHorizontal)
                grid[y+i, x] = word[i];
            else
                grid[y, x+i] = word[i];
        }
        placement.Add(word, (x, y, isHorizontal));
        gridWidth = Math.Max(gridWidth, x + (isHorizontal ? word.Length : 0) + 10);
        gridHeight = Math.Max(gridHeight, y + (!isHorizontal ? word.Length : 0) + 10);
        char[,] newGrid = new char[gridHeight, gridWidth];
        for (int r = 0; r < Math.Min(newGrid.GetLength(0), grid.GetLength(0)); r++)
            for (int c = 0; c < Math.Min(newGrid.GetLength(1), grid.GetLength(1)); c++)
                newGrid[r, c] = grid[r, c];
        grid = newGrid;
    }
}
```

5) BuildCrissCrossPuzzle
Данный метод берет первое слово и размещает его в начале сетки по горизонтали. Далее для каждого последующего слова он вызывает метод PlaceWord, чтобы уже для введенного слова подобрать подходящее место. И в конце происходит вызов метода DrawPuzzle, чтобы отображать сетку на экране.
*Приведем реализацию данного метода:*
```
private void BuildCrissCrossPuzzle()
{
    if (words.Count == 0)
        return;
    string firstWord = words[0];
    int startX = 10;
    int startY = 10;
    placement.Add(firstWord, (startX, startY, true));
    gridWidth = Math.Max(gridWidth, startX + firstWord.Length + 10);
    gridHeight = Math.Max(gridHeight, startY + 10);
    grid = new char[gridHeight, gridWidth];
    for (int i = 0; i < firstWord.Length; i++)
        grid[startY, startX + i] = firstWord[i];
    for (int i = 1; i < words.Count; i++)
        PlaceWord(words[i]);
    DrawPuzzle();
}
```

6) Button_Click
Данный метод вызывается при нажатии на кнопку Add. Он добавляет слово из текстового поля wordTextBox в список words.
*Приведем реализацию данного метода:*
```
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (!string.IsNullOrEmpty(wordTextBox.Text))
    {
        words.Add(wordTextBox.Text);
        wordTextBox.Clear();
    }
}
```

7) Button_Click1
Данный метод вызывается при нажатии на кнопку Draw. Он создает сам крисс-кросс. При этом он очищает холст, словарь placement, сетку и далее вызывает метод BuildCrissCrossPuzzle.
*Приведем реализацию данного метода:*
```
private void Button_Click_1(object sender, RoutedEventArgs e)
{
    gridCanvas.Children.Clear();
    placement.Clear();
    if (words.Count == 0)
    {
        MessageBox.Show("Add words to building");
        return;
    }
    grid = null;
    gridWidth = 0;
    gridHeight = 0;
    BuildCrissCrossPuzzle();
}
```

Таким образом, выше описан алгоритм, по которому троится головоломка крисс-кросс. Далее ниже приведем пример работы данного алгоритма.

Первым делом введем слово для добавления, нажмем кнопку Add и кнопку Draw.
![[Pasted image 20250105170225.png]]

Далее можем добавить еще слов и посмотреть на работу алгоритма. Наблюдаем корректную работу алгоритма и оптимизированное построение головоломки.
![[Pasted image 20250105170501.png]]

