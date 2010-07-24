/////////////////
///LostSinner///
///////////////
using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a DragonKnight Mage corpse" )] 
	public class DragonKnightMage : BaseCreature 
	{
		[Constructable]
		public DragonKnightMage() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 ) 
		{
 			Title = "a DragonKnight Mage";
			Name = NameList.RandomName( "male" );
			Body = 0x191;
			Hue = 33805;
			Female = false;

			SetStr( 1025, 1425 );
			SetDex( 81, 148 );
			SetInt( 475, 675 );
			
			Fame = 1000;
			Karma = 1000;
			
			SetHits( 1000, 2000 );

			SetDamage( 24, 33 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance( ResistanceType.Physical, 60, 85 );
			SetResistance(ResistanceType.Fire, 65, 90);
            SetResistance(ResistanceType.Cold, 40, 55);
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 50, 75 );

			SetSkill( SkillName.Archery, 100, 140 );
			SetSkill( SkillName.Tactics, 100, 140 );
			SetSkill( SkillName.MagicResist, 100, 140 );
			SetSkill( SkillName.Tactics, 100, 140 );
			SetSkill( SkillName.Wrestling, 100, 140 );
			SetSkill( SkillName.Swords, 100, 140 );
			SetSkill( SkillName.Magery, 100, 140 );
			SetSkill( SkillName.Focus, 100, 140 );
			SetSkill( SkillName.EvalInt, 100, 140 );

			VirtualArmor = 80;
   			
   			GlacialStaff glacialstaff = new GlacialStaff();
   			glacialstaff.Movable = false;
   			AddItem(glacialstaff);
   			
   			HumilityCloak cloak = new HumilityCloak();
   			cloak.Movable = false;
   			AddItem(cloak);
			
   			SpiritualityHelm spiritualityhelm = new SpiritualityHelm();
			spiritualityhelm.Movable = false;
   			AddItem(spiritualityhelm);

   			HonestyGorget honestygorget = new HonestyGorget();
			honestygorget.Movable = false;
   			AddItem(honestygorget);
   			
   			JusticeBreastplate justicebreastplate = new JusticeBreastplate();
			justicebreastplate.Movable = false;
   			AddItem(justicebreastplate);

   			BodySash bodysash = new BodySash();
			bodysash.Movable = false;
			bodysash.Name = "Royal DragonKnight";
   			AddItem(bodysash);
   			
   			CompassionArms compassionarms = new CompassionArms();
			compassionarms.Movable = false;
   			AddItem(compassionarms);
   			
   			ValorGauntlets valorgauntlets = new ValorGauntlets();
			valorgauntlets.Movable = false;
   			AddItem(valorgauntlets);
   			
   			HonorLegs honorlegs = new HonorLegs();
			honorlegs.Movable = false;
   			AddItem(honorlegs);
   			
   			SacrificeSollerets sacrificesollerets = new SacrificeSollerets();
   			sacrificesollerets.Movable = false;
   			AddItem(sacrificesollerets);

   			DraculasShroud draculasshroud = new DraculasShroud();
   			draculasshroud.Movable = false;
   			AddItem(draculasshroud);

   			
   			
//   		PlateChest chest = new PlateChest();
//			chest.Movable = false;
//			chest.Hue = 137;
//   		AddItem(chest);
   			

			VampiriacSteed vampiriacsteed = new VampiriacSteed();
        	vampiriacsteed.Hue = 2219;
			//horse.Hits = 200;
			//horse.Karma = 500;
        	vampiriacsteed.Rider = this;
        	
        	//new VampiriacSteed().Rider = this;
   			glacialstaff.Hue = 1152;
   			cloak.Hue = 253;
			spiritualityhelm.Hue = 2406;
			honestygorget.Hue = 2406;
			justicebreastplate.Hue = 2406;
			bodysash.Hue = 253;
			compassionarms.Hue = 2406;
			valorgauntlets.Hue = 2406;
			honorlegs.Hue = 2406;
			sacrificesollerets.Hue = 2406;
   			draculasshroud.Hue = 2406;

   			
		}

		public override void GenerateLoot()
		{			
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Gems, 5 ); 
		}
		public override bool ShowFameTitle{ get{ return false; } }

		public override bool OnBeforeDeath()
        {
//            switch (Utility.Random(30))
//            {
//                case 0: PackItem(new VampireLeatherChest()); break;
//                case 1: PackItem(new VampireLeatherGloves()); break;
//                case 2: PackItem(new VampireLeatherGorget()); break;
//                case 3: PackItem(new VampireLeatherLegs()); break;
//                case 4: PackItem(new VampireLeatherArms()); break;
//
//            }
//            PackGold(50, 100);
            IMount mount = this.Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Delete();

            if (Utility.RandomDouble() < 0.3)
            {
                Mobile mob = new VampiriacSteed();
                mob.Hue = 2219;
                mob.Name = "a DragonKnight steed";
                mob.MoveToWorld(this.Location, this.Map);
                mob.Direction = this.Direction;
            }

            return base.OnBeforeDeath();
        }

		public override bool CanRummageCorpses{ get{ return true; } }
//		public override bool AlwaysMurderer{ get{ return true; } }

		public DragonKnightMage( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
