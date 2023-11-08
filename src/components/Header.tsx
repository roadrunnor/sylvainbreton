// header.tsx
import React from "react";
import "../scss/_header.scss";

const Header = () => {
	return (
		<header>
			<div className="container">
				
				<div className="row">
					{/* First column */}
					<div className="col">
						<blockquote>
							<p>SYLVAIN BRETON</p>
							<span> ______________________________________ </span>
						</blockquote>
					</div>
					{/* Second column */}
					<div className="col">
						<blockquote>
							<span> ______________________________________ </span>
						</blockquote>
					</div>
					{/* Third column */}
					<div className="col">
						<blockquote>
							<span> ______________________________________ </span>
						</blockquote>
					</div>
					{/* Fourth column */}
					<div className="col">
						<div>
							<div className="head-marquee">
								<div className="scrolling-text">
									<em>
										"THE ONLY THING SELF-EVIDENT ABOUT THE CONTEMPORARY IS THAT
										NOTHING CONCERNING THE CONTEMPORARY IS SEFT-EVIDENT ANYMORE"
									</em>{" "}
									- Joao Ribas, "What To Do With The Contemporary?",
									<em> Ten Fundamental Questions of Curating</em>, Fiorucci Art
									Trust with Mousse Editions, Milan, Italy. 2011, p. 67. (From
									Adorno Statement :
									<em>
										"IT IS SELF-EVIDENT THAT NOTHING CONCERNING ART IS
										SEFL-EVIDENT ANYMORE, NOT ITS INNER LIFE, NOT ITS
										RELATIONSHIP TO THE WORLD, NOT EVEN ITS RIGHT TO EXIST "
									</em>
									, Theodor Adorno, <em>Aesthetic Theory.</em>
								</div>
							</div>
							<span>______________________________________</span>
						</div>
					</div>
				</div>

				<div className="row">
					<div className="col">
						<blockquote>
							<p>Background : Don Brown</p>
						</blockquote>
					</div>
				</div>

			</div>
		</header>
	);
};

export default Header;
