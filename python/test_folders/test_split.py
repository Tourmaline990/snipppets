from src.split import analyzeText
import pytest
from pytest import approx

def test_extractText():
    text =  analyzeText("  apple,banana,: ; orange,grape!practicalprompt ") 
    assert text.extractText()  ==  ['apple', 'banana', 'orange', 'grape', 'practicalprompt']

def test_validateText():
     text = analyzeText("  apple,banana,: ; orange,grape!practicalprompt ") 
     assert text.validateText("  apple,banana,: ; orange,grape!practicalprompt ") == "  apple,banana,: ; orange,grape!practicalprompt "
     
     # assert empty text
     with pytest.raises(TypeError) as validate_failed:
          text.validateText("")
     assert "Text cannot be empty" in str(validate_failed.value)

def test_evaluateText():
     text =  analyzeText("  apple,banana,: ; orange,grape!practicalprompt ") 
     assert text.evaluateText() == {'total words': 5, 'total words length': 37, 'longest word length': 15, 'longest word name': 'practicalprompt'}

def test_most_common_word():
      # assert first instance
     text = analyzeText("  apple,banana,: ; orange,grape!practicalprompt ")
     assert text.mostCommonWord() == {'most occured': 'same occurence(1)'}

      # assert second instance
     text1 = analyzeText("  apple,banana,: ; orange,grape!practicalprompt-apple ") 
     assert text1.mostCommonWord() == {'most occured': 'apple(2)'}

def test_Dict():
     # assert first instance
     text1 = analyzeText("  apple,banana,: ; orange,grape!practicalprompt-apple ") 
     text1.validateText("  apple,banana,: ; orange,grape!practicalprompt-apple ")
     text1.extractText()
     text1.evaluateText()
     text1.mostCommonWord()
     assert text1.Dict() == {'total words': 6, 'total words length': 42, 'longest word length': 15, 'longest word name': 'practicalprompt', 'most occured': 'apple(2)'}

     # assert second instance
     text2 = analyzeText("  apple,banana,: ; orange,grape!practicalprompt ") 
     assert text2.Dict() == {}

 

# pytest.main(["-v","--tb=line","-rN","__file__"])