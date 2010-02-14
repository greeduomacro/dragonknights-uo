using System;

namespace Server.Items
{
	public class Statue230 : Item
	{
		[Constructable]
		public Statue230() : base( 0x20F3 )
		{
			Weight = 1.0;
		}

		public Statue230( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
