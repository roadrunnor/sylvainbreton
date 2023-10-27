import React from 'react';

const Header = () => {
  return (
    <header>
      <div className="container">
        <div className="row">
          <div className="col-md-12 col-lg-4">
            <blockquote>
              <p>SYLVAIN BRETON</p>
              <span className="unp"> ______________________________________ </span>
            </blockquote>
          </div>
          <div className="col-md-12 col-lg-4">
            <p className="style4 Titre unp">
              <br />
              <span className="unp"> ______________________________________ </span>
              <em>
                <strong>
                  <br />
                </strong>
              </em>
            </p>
          </div>
          <div className="col-md-12 col-lg-4">
            <div className="style4">
              <p className="head-marquee">
                <marquee className="marquee" direction="left" loop="true">
                  <em>
                    "THE ONLY THING SELF-EVIDENT ABOUT THE CONTEMPORARY IS THAT
                    NOTHING CONCERNING THE CONTEMPORARY IS SEFT-EVIDENT ANYMORE"
                  </em>{' '}
                  - Joao Ribas, "What To Do With The Contemporary?",{' '}
                  <em>Ten Fundamental Questions of Curating</em>, Fiorucci Art
                  Trust with Mousse Editions, Milan, Italy. 2011, p. 67. (From
                  Adorno Statement :{' '}
                  <em>
                    "IT IS SELF-EVIDENT THAT NOTHING CONCERNING ART IS
                    SEFL-EVIDENT ANYMORE, NOT ITS INNER LIFE, NOT ITS RELATIONSHIP
                    TO THE WORLD, NOT EVEN ITS RIGHT TO EXIST "
                  </em>
                  , Theodor Adorno, <em>Aesthetic Theory.</em>
                </marquee>
              </p>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
