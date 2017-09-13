﻿using System ;
using System . Collections ;
using System . Collections . Generic ;
using System . IO ;
using System . Linq ;
using System . Reflection ;
using System . Text ;

namespace WenceyWang . FIGlet
{

	public class FIGletFont
	{

		public string Signature { get ; }

		public char HardBlank { get ; }

		public int Height { get ; }

		public int BaseLine { get ; }

		public int MaxLength { get ; }

		public int OldLayout { get ; }

		public int CommentLines { get ; }

		public int PrintDirection { get ; }

		public int FullLayout { get ; }

		public int CodeTagCount { get ; }

		public string [ ] [ ] Lines { get ; }

		public string Commit { get ; }

		public static FIGletFont Defult
		{
			get
			{
				lock ( DefultFont )
				{
					FIGletFont defult = null ;
					DefultFont ? . TryGetTarget ( out defult ) ;
					if ( defult == null )
					{
						Stream stream = typeof ( FIGletFont ) . GetTypeInfo ( ) .
																Assembly . GetManifestResourceStream ( typeof ( FIGletFont ) . Namespace
																										+ "."
																										+ @"Fonts.standard.flf" ) ;
						defult = new FIGletFont ( stream ) ;
						stream ? . Dispose ( ) ;
						DefultFont . SetTarget ( defult ) ;
					}
					return defult ;
				}
			}
		}

		public FIGletFont ( Stream fontStream )
		{
			using ( StreamReader reader = new StreamReader ( fontStream ) )
			{
				string [ ] configs = reader . ReadLine ( ) . Split ( ' ' ) ;
				if ( ! configs [ 0 ] . StartsWith ( @"flf2a" ) )
				{
					throw new ArgumentException ( $"{nameof(fontStream)} missing signature" , nameof(fontStream) ) ;
				}

				Signature = @"flf2a" ;
				HardBlank = configs [ 0 ] . Last ( ) ;
				try
				{
					Height = Convert . ToInt32 ( TryGetMember ( configs , 1 ) ) ;
					BaseLine = Convert . ToInt32 ( TryGetMember ( configs , 2 ) ) ;
					MaxLength = Convert . ToInt32 ( TryGetMember ( configs , 3 ) ) ;
					OldLayout = Convert . ToInt32 ( TryGetMember ( configs , 4 ) ) ;
					CommentLines = Convert . ToInt32 ( TryGetMember ( configs , 5 ) ) ;
					PrintDirection = Convert . ToInt32 ( TryGetMember ( configs , 6 ) ) ;
					FullLayout = Convert . ToInt32 ( TryGetMember ( configs , 7 ) ) ;
					CodeTagCount = Convert . ToInt32 ( TryGetMember ( configs , 8 ) ) ;
				}
				catch ( IndexOutOfRangeException )
				{
				}

				StringBuilder commentBuilder = new StringBuilder ( ) ;
				for ( int lineCount = 0 ; lineCount < CommentLines ; lineCount++ )
				{
					commentBuilder . AppendLine ( reader . ReadLine ( ) ) ;
				}

				Commit = commentBuilder . ToString ( ) ;

				Lines = new string[ 256 ] [ ] ;

				int currentChar = 32 ;

				while ( ! reader . EndOfStream )
				{
					int charIndex ;
					string currentLine = reader . ReadLine ( ) ?? string . Empty ;
					if ( int . TryParse ( currentLine , out charIndex ) )
					{
						currentChar = charIndex ;
					}

					Lines [ currentChar ] = new string[ Height ] ;

					int currentLineIndex = 0 ;
					while ( currentLineIndex < Height )
					{
						Lines [ currentChar ] [ currentLineIndex ] = currentLine . TrimEnd ( '@' ) . Replace ( HardBlank , ' ' ) ;

						if ( currentLine . EndsWith ( @"@@" ) )
						{
							break ;
						}

						currentLine = reader . ReadLine ( ) ?? string . Empty ;
						currentLineIndex++ ;
					}

					currentChar++ ;
				}
			}
		}

		private static readonly WeakReference <FIGletFont> DefultFont = new WeakReference <FIGletFont> ( null ) ;

		private static T TryGetMember <T> ( T [ ] array , int index )
		{
			if ( index < array . Length )
			{
				return array [ index ] ;
			}

			return default ( T ) ;
		}

		public string GetCharacter ( char sourceChar , int line )
		{
			if ( line < 0
				|| line >= Height )
			{
				throw new ArgumentOutOfRangeException ( nameof(line) ) ;
			}

			if ( Lines [ Convert . ToInt16 ( sourceChar ) ] == null )
			{
				return string . Empty ;
			}

			return Lines [ Convert . ToInt16 ( sourceChar ) ] [ line ] ;
		}

		//private void LoadLines ( List<string> fontLines )
		//{
		//	Lines = fontLines;
		//	string configString = Lines . First ( );
		//	string [ ] configArray = configString . Split ( ' ' );
		//	Signature = configArray . First ( ) . Remove ( configArray . First ( ) . Length - 1 );
		//	if ( Signature == "flf2a" )
		//	{
		//HardBlank = configArray . First ( ) . Last ( ) . ToString ( );
		//Height = configArray . GetIntValue ( 1 );
		//BaseLine = configArray . GetIntValue ( 2 );
		//MaxLength = configArray . GetIntValue ( 3 );
		//OldLayout = configArray . GetIntValue ( 4 );
		//CommentLines = configArray . GetIntValue ( 5 );
		//PrintDirection = configArray . GetIntValue ( 6 );
		//FullLayout = configArray . GetIntValue ( 7 );

		//CodeTagCount = configArray . GetIntValue ( 8 );

		//	}
		//}


		//private void LoadFont ( Stream fontStream )
		//{
		//	List<string> fontData = new List<string> ( );

		//	using ( StreamReader reader = new StreamReader ( fontStream ) )
		//	{
		//		while ( !reader . EndOfStream )
		//		{
		//			fontData . Add ( reader . ReadLine ( ) );
		//		}
		//	}

		//	LoadLines ( fontData );
		//}

	}

}
