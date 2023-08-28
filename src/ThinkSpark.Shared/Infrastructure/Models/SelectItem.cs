namespace ThinkSpark.Shared.Infrastructure.Models
{
    public class SelectItem
    {
        /// <summary>
        /// Campo utilizando para separar itens quando eles por algum motivo tiverem o mesmo Value.
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// Descrição o item.
        /// </summary>
        public string Text { get; set; } = null!;

        /// <summary>
        /// Valor do item pode ser um número ou texto.
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Informa se o item sera o selecionado ou não na combobox.
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// Caso o item tenha sub-itens, podem ser adicionados aqui.
        /// </summary>
        public List<SelectItem>? SubItens { get; set; } = new List<SelectItem>();

        /// <summary>
        /// Construtor.
        /// </summary>
        public SelectItem() { }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="key">Campo utilizando para separar itens quando eles por algum motivo tiverem o memso Value.</param>
        /// <param name="text">Descrição o item.</param>
        public SelectItem(long key, string text)
        {
            Key = key;
            Text = text;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="key">Campo utilizando para separar itens quando eles por algum motivo tiverem o memso Value.</param>
        /// <param name="text">Descrição o item.</param>
        /// <param name="value">Valor do item pode ser um número ou texto.</param>
        /// <param name="selected">Informa se o item sera o selecionado ou não na combobox.</param>
        /// <param name="subItens">Caso o item tenha sub-itens, podem ser adicionados aqui.</param>
        public SelectItem(long key, string text, object? value = null, bool selected = false, List<SelectItem>? subItens = default)
        {
            Key = key;
            Text = text;
            Value = value;
            Selected = selected;
            SubItens = subItens;
        }
    }
}
