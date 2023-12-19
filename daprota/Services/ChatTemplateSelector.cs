using daprota.Models;

namespace daprota.Services
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ChatMsgBotTemplate { get; set; }
        public DataTemplate ChatMsgUserTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is M_ChatMsg chat)
            {
                if(chat.Name == "Bot")
                {
                    return ChatMsgBotTemplate;
                }
                else
                {
                     return ChatMsgUserTemplate;
                }
            }
            throw new NotImplementedException();
        }
    }
}
