import React from "react";
import "../scss/_header.scss";

const Header = () => {
	return (
		<header className="header">
			<div className="container">
				<div className="row">
					
					<div className="col">
						<p>SYLVAIN BRETON</p>
						<div className="underline" />
					</div>
					<div className="col">
						<div className="underline" />
					</div>
					<div className="col">
						<div className="underline" />
					</div>
					<div className="col">
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
									SEFL-EVIDENT ANYMORE, NOT ITS INNER LIFE, NOT ITS RELATIONSHIP
									TO THE WORLD, NOT EVEN ITS RIGHT TO EXIST")
								</em>
								, Theodor Adorno, <em>Aesthetic Theory.</em>
							</div>
						</div>
						<div className="underline" />
					</div>
					
				</div>

				<div className="row">
					<div className="col">
						<p>Background : Don Brown</p>
					</div>
				</div>
			</div>
		</header>
	);
};

export default Header;
