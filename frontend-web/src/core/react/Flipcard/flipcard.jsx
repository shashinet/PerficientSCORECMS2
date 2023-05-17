import React from 'react';

const Flipcard = () => (

  <div className="wrapper flipcard">
    <div className="card">
      <input type="checkbox" id="card1" className="more flip-right" aria-hidden="true" />
      <div className="content">
        <div
          className="front"
        >
          <div className="inner">
            <img src="" alt="" className="front-small-image" />
            <img src="" alt="" className="front-mask-image" />
            <h2>Flip Card</h2>
            <div>
              <div className="rich-text ">
                <p>This is the flip cards basic component. Keep</p>
                <p>Keep the text to under 10 words.</p>
              </div>
            </div>
            <label htmlFor="card1" className="view-details" aria-hidden="true">
              Details
            </label>
          </div>
        </div>
        <div
          className="back flip-right"

        >
          <div className="inner">
            <img src="" alt="" className="back-small-image" />
            <img src="" alt="" className="back-mask-image" />
            <label htmlFor="card1" className="backlable" aria-hidden="true" />
            <div className="info">
              <div className="rich-text ">
                <p>Flip cards details uses solid colors as well as background image.</p>
                <p>Approximately 10 words or 2 lines is strongly recommended.</p>
                <p>Be sure to test your front text length in tablet sizes.</p>
              </div>
            </div>
            <div className="view-more">
              <a
                className="score-button GTMBUTTON"
                target="_blank"
                rel="noreferrer"
                title="View More"
                href="http://www.google.com/"
              >
                View More
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

);

export default Flipcard;
