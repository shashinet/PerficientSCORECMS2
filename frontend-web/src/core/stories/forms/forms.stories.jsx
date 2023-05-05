/* eslint-disable indent */
/* eslint-disable spaced-comment */
/* eslint-disable no-multiple-empty-lines */
/* eslint-disable no-trailing-spaces */
/* eslint-disable jsx-a11y/label-has-associated-control */
/* eslint-disable react/self-closing-comp */
/* eslint-disable react/style-prop-object */
/* eslint-disable react/no-unknown-property */
/* eslint-disable max-len */
/* eslint-disable react/jsx-one-expression-per-line */
/* eslint-disable react/jsx-indent */
import React from 'react';
import './forms.scss';

export default {
  title: 'Forms/Forms',
};

export const Standard = () => {

  return (
    <form method="post" novalidate="novalidate" data-f-metadata="" enctype="multipart/form-data"
          class="EPiServerForms ValidationSuccess" data-f-type="form"
          id="0e083bcd-76b5-4aaa-9d81-6fd9c0759f61">
      <script type="text/javascript"
              src="/EPiServer.Forms/DataSubmit/GetFormInitScript?formGuid=0e083bcd-76b5-4aaa-9d81-6fd9c0759f61&amp;formLanguage=en"></script>
      <input type="hidden" class="Form__Element Form__SystemElement FormHidden FormHideInSummarized"
             name="__FormGuid" value="0e083bcd-76b5-4aaa-9d81-6fd9c0759f61" data-f-type="hidden"
             autocomplete="off"/>
      <input type="hidden" class="Form__Element Form__SystemElement FormHidden FormHideInSummarized"
             name="__FormHostedPage" value="665" data-f-type="hidden" autocomplete="off"/>
      <input type="hidden" class="Form__Element Form__SystemElement FormHidden FormHideInSummarized"
             name="__FormLanguage" value="en" data-f-type="hidden" autocomplete="off"/>
      <input type="hidden" class="Form__Element Form__SystemElement FormHidden FormHideInSummarized"
             name="__FormCurrentStepIndex" value="0" data-f-type="hidden" autocomplete="off"/>
      <input type="hidden" class="Form__Element Form__SystemElement FormHidden FormHideInSummarized"
             name="__FormSubmissionId" value="" data-f-type="hidden" autocomplete="off"/>
      <input name="__RequestVerificationToken" type="hidden"
             value="pdrFNPFaCuKYcLU6FnXM8Qtono3LLDTdFi9OHBd7WAvikVRDhqf8FUnE08yEjtfgTH-ip9qMKVjWmpCfsdD-rze3q3SPwNFfFu0suO2dr7g1"/>
      <aside class="Form__Description">* indicates required field</aside>
      <div class="Form__Status">
        <div role="alert" aria-live="polite" class="Form__Status__Message hide"
             data-f-form-statusmessage="">
        </div>
      </div>
      <div data-f-mainbody="" class="Form__MainBody">
        <section id="__field_" data-f-type="step" data-f-element-name="__field_"
                 class="Form__Element FormStep Form__Element--NonData " data-f-stepindex="0"
                 data-f-element-nondata="">
          <div class="Form__Element FormTextbox ValidationRequired"
               data-f-element-name="__field_378" data-f-type="textbox">
            <label for="00520e46-89f0-46ab-bb72-4d359b533592" class="Form__Element__Caption">First
              Name *</label>
            <input name="__field_378" id="00520e46-89f0-46ab-bb72-4d359b533592" type="text"
                   class="FormTextbox__Input" aria-describedby="__field_378_desc"
                   placeholder="First Name" value="" required="" aria-required="true"
                   data-f-datainput="" aria-invalid="false"/>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_378"
                  data-f-validationerror="" id="__field_378_desc"></span>
          </div>
          <div class="Form__Element FormTextbox ValidationRequired"
               data-f-element-name="__field_379" data-f-type="textbox">
            <label for="c37ac551-6529-447b-b785-a42ca21555e2" class="Form__Element__Caption">Last
              Name *</label>
            <input name="__field_379" id="c37ac551-6529-447b-b785-a42ca21555e2" type="text"
                   class="FormTextbox__Input" aria-describedby="__field_379_desc"
                   placeholder="Last Name" value="" required="" aria-required="true"
                   data-f-datainput="" aria-invalid="false"/>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_379"
                  data-f-validationerror="" id="__field_379_desc"></span>
          </div>
          <div class="Form__Element FormTextbox ValidationRequired"
               data-f-element-name="__field_383" data-f-type="textbox">
            <label for="4fc01512-8d62-4bde-a9d6-85478e46900a" class="Form__Element__Caption">Email
              *</label>
            <input name="__field_383" id="4fc01512-8d62-4bde-a9d6-85478e46900a" type="text"
                   class="FormTextbox__Input" aria-describedby="__field_383_desc"
                   placeholder="Email" value="" required="" aria-required="true" data-f-datainput=""
                   aria-invalid="false"/>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_383"
                  data-f-validationerror="" id="__field_383_desc"></span>
          </div>
          <div class="Form__Element FormTextbox ValidationRequired"
               data-f-element-name="__field_384" data-f-type="textbox">
            <label for="e9322570-46fc-45ba-809b-629f4c83832b" class="Form__Element__Caption">Phone
              *</label>
            <input name="__field_384" id="e9322570-46fc-45ba-809b-629f4c83832b" type="text"
                   class="FormTextbox__Input" aria-describedby="__field_384_desc"
                   placeholder="(###) ###-####" value=""
                   title="By providing your mobile phone number, you are consenting to receive text messages from Site 2. You can unsubscribe at any time by texting &quot;Stop.&quot; Texts are free, though standard text message fees from your cell phone service provider may apply. See privacy policy for details."
                   required="" aria-required="true" data-f-datainput="" aria-invalid="false"/>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_384"
                  data-f-validationerror="" id="__field_384_desc"></span>
          </div>
          <div class="Form__Element FormSelection ValidationRequired"
               data-f-element-name="__field_467" data-f-type="selection">
            <label for="33928c1d-b528-4119-a102-ac05adbd7f68"
                   class="Form__Element__Caption">Discipline</label>
            <select name="__field_467" id="33928c1d-b528-4119-a102-ac05adbd7f68" required=""
                    aria-required="true" data-f-datainput="" aria-describedby="__field_467_desc"
                    aria-invalid="false">
              <option disabled="disabled" selected="selected" value="">-- Select an option --
              </option>
              <option value="Occupational Therapist" data-f-datainput="">Occupational Therapist
              </option>
              <option value="Physical Therapist" data-f-datainput="">Physical Therapist</option>
              <option value="Speech/Language Pathologist" data-f-datainput="">Speech/Language
                Pathologist
              </option>
            </select>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_467"
                  data-f-validationerror="" id="__field_467_desc"></span>
          </div>
          <div class="Form__Element FormSecondarySelection FormSelection ValidationRequired"
               data-f-element-name="__field_470" data-f-type="selection">
            <label for="cbc8113f-8a9b-492a-b6a4-5c822514e080" class="Form__Element__Caption">Specialty
              *</label>
            <select name="__field_470" id="cbc8113f-8a9b-492a-b6a4-5c822514e080" data-website="mt"
                    data-api-url="https://google.com/api/jobs" required=""
                    aria-required="&quot;true&quot;"
                    data-dependent-on="33928c1d-b528-4119-a102-ac05adbd7f68" disabled="disabled">
              <option disabled="disabled" selected="&quot;selected&quot;" value="">--Select an
                option--
              </option>
            </select>
          </div>
          <div data-react-component="SuggestionApiAutoComplete"
               data-props="{&quot;apiUrl&quot;:&quot;https://google.com/api/jobs&quot;,&quot;placeholder&quot;:null,&quot;label&quot;:&quot;University *&quot;,&quot;name&quot;:&quot;__field_472&quot;,&quot;id&quot;:&quot;c5731a8a-8819-4f3a-82f4-63028fcef421&quot;,&quot;type&quot;:&quot;school&quot;,&quot;error&quot;:&quot;&quot;,&quot;initialValue&quot;:null}">
            <div class="Form__Element FormTextbox auto-complete relative"
                 data-f-element-name="__field_472" data-f-type="textbox">
              <label for="c5731a8a-8819-4f3a-82f4-63028fcef421" class="Form__Element__Caption">University
                *</label><input id="c5731a8a-8819-4f3a-82f4-63028fcef421" name="__field_472"
                                type="text" class="FormTextbox__Input" autocomplete="off"
                                role="combobox" aria-label="University *"
                                aria-controls="c5731a8a-8819-4f3a-82f4-63028fcef421-overlay"
                                aria-owns="c5731a8a-8819-4f3a-82f4-63028fcef421-listbox"
                                aria-expanded="false"
                                aria-describedby="c5731a8a-8819-4f3a-82f4-63028fcef421_desc"
                                value=""/>
              <div id="c5731a8a-8819-4f3a-82f4-63028fcef421-overlay"
                   class="auto-complete-container absolute"></div>
              <span data-f-linked-name="__field_472" class="Form__Element__ValidationError"
                    id="__field_472_desc"></span>
            </div>
          </div>
          <div class="Form__Element FormSelection ValidationRequired"
               data-f-element-name="__field_473" data-f-type="selection">
            <label for="6677785b-e7e7-4619-b68c-cf121c450652" class="Form__Element__Caption">University
              State *</label>
            <select name="__field_473" id="6677785b-e7e7-4619-b68c-cf121c450652" required=""
                    aria-required="true" data-f-datainput="" aria-describedby="__field_473_desc"
                    aria-invalid="false">
              <option disabled="disabled" selected="selected" value="">-- Select an option --
              </option>
              <option value="AL" data-f-datainput="">Alabama</option>
              <option value="AK" data-f-datainput="">Alaska</option>
              <option value="AZ" data-f-datainput="">Arizona</option>
              <option value="AR" data-f-datainput="">Arkansas</option>
              <option value="CA" data-f-datainput="">California</option>
              <option value="CO" data-f-datainput="">Colorado</option>
              <option value="CT" data-f-datainput="">Connecticut</option>
              <option value="DE" data-f-datainput="">Delaware</option>
              <option value="DC" data-f-datainput="">District Of Columbia</option>
              <option value="FL" data-f-datainput="">Florida</option>
              <option value="GA" data-f-datainput="">Georgia</option>
              <option value="HI" data-f-datainput="">Hawaii</option>
              <option value="ID" data-f-datainput="">Idaho</option>
              <option value="IL" data-f-datainput="">Illinois</option>
              <option value="IN" data-f-datainput="">Indiana</option>
              <option value="IA" data-f-datainput="">Iowa</option>
              <option value="KS" data-f-datainput="">Kansas</option>
              <option value="KY" data-f-datainput="">Kentucky</option>
              <option value="LA" data-f-datainput="">Louisiana</option>
              <option value="ME" data-f-datainput="">Maine</option>
              <option value="MD" data-f-datainput="">Maryland</option>
              <option value="MA" data-f-datainput="">Massachusetts</option>
              <option value="MI" data-f-datainput="">Michigan</option>
              <option value="MN" data-f-datainput="">Minnesota</option>
              <option value="MS" data-f-datainput="">Mississippi</option>
              <option value="MO" data-f-datainput="">Missouri</option>
              <option value="MT" data-f-datainput="">Montana</option>
              <option value="NE" data-f-datainput="">Nebraska</option>
              <option value="NV" data-f-datainput="">Nevada</option>
              <option value="NH" data-f-datainput="">New Hampshire</option>
              <option value="NJ" data-f-datainput="">New Jersey</option>
              <option value="NM" data-f-datainput="">New Mexico</option>
              <option value="NY" data-f-datainput="">New York</option>
              <option value="NC" data-f-datainput="">North Carolina</option>
              <option value="ND" data-f-datainput="">North Dakota</option>
              <option value="OH" data-f-datainput="">Ohio</option>
              <option value="OK" data-f-datainput="">Oklahoma</option>
              <option value="OR" data-f-datainput="">Oregon</option>
              <option value="PA" data-f-datainput="">Pennsylvania</option>
              <option value="RI" data-f-datainput="">Rhode Island</option>
              <option value="SC" data-f-datainput="">South Carolina</option>
              <option value="SD" data-f-datainput="">South Dakota</option>
              <option value="TN" data-f-datainput="">Tennessee</option>
              <option value="TX" data-f-datainput="">Texas</option>
              <option value="UT" data-f-datainput="">Utah</option>
              <option value="VT" data-f-datainput="">Vermont</option>
              <option value="VA" data-f-datainput="">Virginia</option>
              <option value="WA" data-f-datainput="">Washington</option>
              <option value="WV" data-f-datainput="">West Virginia</option>
              <option value="WI" data-f-datainput="">Wisconsin</option>
              <option value="WY" data-f-datainput="">Wyoming</option>
            </select>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_473"
                  data-f-validationerror="" id="__field_473_desc"></span>
          </div>
          <div class="Form__Element FormTextbox ValidationRequired"
               data-f-element-name="__field_474" data-f-type="textbox">
            <label for="42560c98-82e3-4415-80a1-38bd20d798e5" class="Form__Element__Caption">Graduation
              Date *</label>
            <input name="__field_474" id="42560c98-82e3-4415-80a1-38bd20d798e5" type="text"
                   class="FormTextbox__Input" aria-describedby="__field_474_desc"
                   placeholder="MM/DD/YYYY" value="" required="" aria-required="true"
                   data-f-datainput="" aria-invalid="false"/>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_474"
                  data-f-validationerror="" id="__field_474_desc"></span>
          </div>
          <div class="Form__Element FormTextbox FormTextbox--Textarea"
               data-f-element-name="__field_464" data-f-modifier="textarea" data-f-type="textbox">
            <label for="6773169b-bde6-4149-b3d3-73b818623a54"
                   class="Form__Element__Caption">Comments</label>
            <textarea name="__field_464" id="6773169b-bde6-4149-b3d3-73b818623a54"
                      class="FormTextbox__Input" data-f-label="Comments" data-f-datainput=""
                      aria-describedby="__field_464_desc" aria-invalid="false"></textarea>
            <span class="Form__Element__ValidationError" data-f-linked-name="__field_464"
                  data-f-validationerror="" id="__field_464_desc"></span>
          </div>
          <input name="__field_475" id="b1ccb7c6-0cc5-4880-9d64-8397de660e61" type="hidden"
                 value="New Grad Program" class="Form__Element FormHidden FormHideInSummarized"
                 data-f-type="hidden"/>
          <input name="__field_557" id="4916b049-8d95-467a-8d27-17cad39b0308" type="hidden"
                 value="true" class="Form__Element FormHidden FormHideInSummarized"
                 data-f-type="hidden"/>
          <div class="Form__Element FormParagraphText Form__Element--NonData"
               data-f-element-name="__field_388" data-f-element-nondata="">
            <div id="420040e1-07d2-4b03-a991-380fe2c08fd9">
              <p>By clicking ???SUBMIT??? I agree to receive emails, automated text messages and
                phone calls (including calls that contain prerecorded content) from and on behalf of
                Site 1, its parent, and affiliates. I understand these messages
                will be to the email or phone number provided, and will be about employment
                opportunities, positions in which I???ve been placed, and my employment with Site1
                companies. See <a
                  href="dev/src/Web/wwwroot/src/core/stories/forms/forms.stories?_ga=2.247278020.1564555810.1597679426-789033695.1597173940">privacy
                  policy</a> or <a
                  href="dev/src/Web/wwwroot/src/core/stories/forms/forms.stories?_ga=2.247278020.1564555810.1597679426-789033695.1597173940">cookie
                  policy</a> for more details.</p>
            </div>
          </div>
          <button id="672d0721-8374-4c1c-a50d-4d6e26037099" name="submit" type="submit"
                  value="672d0721-8374-4c1c-a50d-4d6e26037099" data-f-is-finalized="false"
                  data-f-is-progressive-submit="true" data-f-type="submitbutton"
                  data-f-element-name="__field_389"
                  class="Form__Element FormExcludeDataRebind FormSubmitButton">
            Submit
          </button>
        </section>
      </div>
    </form>
  );
};
