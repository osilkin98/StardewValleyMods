using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;


namespace SDV {
    class ModEntry : Mod {
        public override void Entry(IModHelper helper) {
            PlayerEvents.InventoryChanged += this.PlayerEvents_InventoryChanged;
        }
        /*
         * This routine automatically removes trash that gets added to your inventory which has no sell value
         * So if you're fishing, and you get something like Nuka Cola, it stays in your inventory, but if you get
         * glasses, or a pile of trash, they automatically get deleted. 
         */
        private void PlayerEvents_InventoryChanged(object sender, EventArgsInventoryChanged e)
        {
            if(e.Added.Count > 0) {
                foreach(var item in e.Added) {
                    // this.Monitor.Log($"Item is: {item.Item.DisplayName}, type is {item.Item.getCategoryName()}");
                    if(item.Item.getCategoryName().Equals("Trash") && item.Item.salePrice() == 0)
                    {
                        // this.Monitor.Log($"Attempting to remove {item.Item.DisplayName} from inventory because it is trash.");
                        if(!Game1.player.Items.Remove(item.Item))
                        {
                            // this.Monitor.Log("Failed, moving onto next item.");
                            continue;
                        } else
                        {
                            this.Monitor.Log("Done");
                        }
                    }
                }
            }
        }
    }
}
