using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles 
{
	public class HireMage : BaseHire
	{
		[Constructable]
		public HireMage() : base( AIType.AI_Mage ) 
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Title = "the mage";
			if ( this.Female = Utility.RandomBool() ) 
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else 
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNeutralHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			if( Utility.RandomBool() && !this.Female )
			{
				Item beard = new Item( Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D ) );

				beard.Hue = hair.Hue;
				beard.Layer = Layer.FacialHair;
				beard.Movable = false;

				AddItem( beard );
			}

			SetStr( 61, 75 );
			SetDex( 81, 95 );
			SetInt( 86, 100 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.EvalInt, 100.0, 125 );
			SetSkill( SkillName.Magery, 100, 125 );
			SetSkill( SkillName.Meditation, 100, 125 );
			SetSkill( SkillName.MagicResist, 100, 125 );
			SetSkill( SkillName.Tactics, 100, 125 );
			SetSkill( SkillName.Macing, 100, 125 );

			Fame = 100;
			Karma = 100;

			AddItem( new Shoes( Utility.RandomNeutralHue() ) );
			AddItem( new Shirt());

			AddItem( new Robe( Utility.RandomNeutralHue() ) );
			AddItem( new ThighBoots() );


			PackGold( 20, 100 );
		}
		public override bool ClickTitle{get{return false;}}
		public HireMage( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );// version 
		}

		public override void Deserialize( GenericReader reader ) 
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
