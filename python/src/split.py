import re

text = "  apple,banana,: ; orange,grape!practicalprompt "
text1 = "  apple,banana,: ; orange,grape!practicalprompt-apple "
# fruits = [item.strip() for item in re.split(r"[!,:; ]",text) if item.strip()]
# print(fruits)
# print(len(fruits))

def analyze_text(text):
  count = 0
  word_count = 0
  char_count = 0
  longest_word = 0
  longest_word_name = ""
  if text != None:
        cleaned_text = [item.strip() for item in re.split(r"[!,:;'""/\\|`~*&^.]",text) if item.strip()]
        word_count = len(cleaned_text)        
        for i in cleaned_text:
           char_count += len(i)
           indexCount = len(i)
           if indexCount >= longest_word:
               longest_word = len(i)
               longest_word_name = i
  return f"Word Count: {word_count} Character Count: {char_count} longest Word: {longest_word_name} {longest_word} letters"
    
#print(analyze_text(text))

class analyzeText:
    def __init__(self,text):
        self.text = self.validateText(text)
        self.returnContainer = {}    
    def validateText(self,text):
        if text != None and text.strip() !=  "":
          return text
        raise TypeError("Text cannot be empty")
    def extractText(self):
        new_text = re.findall(r"\b\w+\b",self.text)
        return new_text
    def evaluateText(self):
        new_text = self.extractText()
        self.returnContainer["total words"] = len(new_text)
        for i in new_text:
            self.returnContainer["total words length"] = self.returnContainer.get("total words length",0) + len(i)
            indexInMemory = len(new_text[0])
            if len(i) > indexInMemory:
                indexInMemory = len(i)
                self.returnContainer["longest word length"] = indexInMemory
                self.returnContainer["longest word name"] = i
        return self.returnContainer
    def mostCommonWord(self):
        wordOccurence = {}
        new_text = self.extractText()
        for x in new_text:
            num = 0
            for i in new_text:
                if x == i:
                    num += 1
                    wordOccurence[x] = num
        firstValue = next(iter(wordOccurence.values())) 
        values_key = ''
        for key,value in wordOccurence.items():
            if value >= firstValue:
               firstValue = value
               values_key = key
        if firstValue != 1:
                 self.returnContainer["most occured"] = f"{values_key}({firstValue})"
        else:
                 self.returnContainer["most occured"] = f"same occurence({firstValue})"
        return self.returnContainer

    def Dict(self):
        return self.returnContainer

        
#texts = analyzeText(text)
#print(texts.mostCommonWord())
##texts.evaluateText()
#print(texts.extractText())
#texts.mostCommonWord()
#texts.textDict()
texts1 = analyzeText(text1)
texts1.evaluateText()
print(f"{texts1.mostCommonWord()} most common")
print(f"{texts1.Dict()} Dictionary")
        